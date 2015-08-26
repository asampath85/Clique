using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfilerCommon;
using UserProfilerModel;


namespace UserProfilerService
{
    public class RequestService
    {
        public int GetLocation(string id)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueLocations.SingleOrDefault(res => string.Equals(res.Pincode, id));

                return response == null ? 0 : response.Id;
            }
        }

        public int GetUser(string id)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueUsers.SingleOrDefault(res => string.Equals(res.MobileNo, id));

                return response == null ? 0 : response.Id;
            }
        }

        public List<RequestModel> GetRequest(RequestStatus status)
        {
            List<RequestModel> response = new List<RequestModel>();
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                List<CliqueRequest> requestList = null;
                if (status == RequestStatus.All)
                {
                    requestList = entity.CliqueRequests.ToList();
                }
                else
                {
                    requestList = entity.CliqueRequests.Where(res => (res.Status == (int?)status || res.Status == null)).ToList();
                }

                foreach (var item in requestList)
                {
                    string statusName = "";
                    if ((item.Status ?? 0) == 0)
                        statusName = "New";
                    else if (item.Status == 1)
                        statusName = "Queued";
                    else if (item.Status ==2)
                        statusName = "Processed";
                    else
                        statusName = "Error";

                    response.Add(new RequestModel
                    {
                        Id = item.Id,
                        LocationId = item.LocationId,
                        UserId = item.UserId,
                        Pincode = item.CliqueLocation.Pincode,
                        UserName = item.CliqueUser.Name,
                        MobileNo = item.CliqueUser.MobileNo,
                        BuildingName = item.BuildingName,
                        City = item.CliqueLocation.City,
                        Score = 0,
                        TwitterUserName = item.CliqueUser.TwitterUserName,
                        StatusName = statusName

                    });

                }
                return response;
            }
        }

        public bool UpdateRequestStatus(int status, int requestId)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueRequests.SingleOrDefault(res=>res.Id == requestId);
                response.Status = status;
                entity.SaveChanges();

            }
            return true;
        }

        public RequestModel AddRequest(RequestModel model)
        {
            if (model.LocationId == 0)
            {
                var locationModel = AddLocation(new LocationModel { Pincode = model.Pincode, City = model.City });
                model.LocationId = locationModel.Id;
            }
            if (model.UserId == 0)
            {
                var userModel = AddUser(new UserModel
                {
                    MobileNo = model.MobileNo,
                    EmailId = model.EmailId,
                    Name = model.UserName,
                    TwitterUserName = model.TwitterUserName
                });

                model.UserId = userModel.Id;
            }
            var request = new CliqueRequest
            {
                Address = model.Address,
                BuildingName = model.BuildingName,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                LocationId = model.LocationId,
                UserId = model.UserId

            };
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueRequests.Add(request);
                entity.SaveChanges();
                model.Id = response.Id;

            }
            return model;
        }

        public LocationModel AddLocation(LocationModel model)
        {
            var request = new CliqueLocation
            {
                Pincode = model.Pincode,
                City = model.City

            };
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                entity.CliqueLocations.Add(request);
                entity.SaveChanges();

            }
            model.Id = request.Id;
            return model;
        }

        public bool AddEvent(List<EventModel> modelList)
        {


            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                foreach (var item in modelList)
                {
                    var request = new CliqueEvent
                    {
                        LocationId = item.LocationId,
                        Name = item.Name,
                        Description = item.Description,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Venue = item.Venue,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    entity.CliqueEvents.Add(request);
                }

                entity.SaveChanges();


            }

            return true;
        }

        public UserModel AddUser(UserModel model)
        {
            var request = new CliqueUser
            {
                MobileNo = model.MobileNo,
                EmailId = model.EmailId,
                TwitterUserName = model.TwitterUserName,
                Name = model.Name,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now


            };
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                entity.CliqueUsers.Add(request);
                entity.SaveChanges();

            }
            model.Id = request.Id;
            return model;
        }

        public bool IsEventExist(int locationId)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueEvents.FirstOrDefault(res => res.LocationId == locationId);
                return response != null;
            }
        }

        public bool IsUserTweetExist(int userId)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueUserTweets.FirstOrDefault(res => res.UserId == userId);
                return response != null;
            }

        }

        public void AddTweetAndUser(List<TweetModel> modelList)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                foreach (var item in modelList)
                {
                    var request = new CliqueTweet
                    {
                        PostedAt = item.PostedAt,
                        PostedBy = item.PostedBy,
                        Text = item.Text,
                        ProfileImageURL = item.ProfileImageURL,
                        TweetIdStr = item.TweetIdStr,
                        Score = item.Score,
                        AddedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    var existingItem = entity.CliqueTweets.SingleOrDefault(res => res.TweetIdStr == request.TweetIdStr);
                    if (existingItem == null)
                    {
                        entity.CliqueTweets.Add(request);
                        entity.SaveChanges();
                        item.Id = request.Id;
                    }
                    else
                        item.Id = existingItem.Id;

                    var mappingRequest = new CliqueUserTweet
                    {
                        TweetId = item.Id,
                        UserId = item.UserId,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    entity.CliqueUserTweets.Add(mappingRequest);
                    entity.SaveChanges();
                }

            }
        }

        public void AddTweetAndLocation(List<TweetModel> modelList)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                foreach (var item in modelList)
                {
                    var request = new CliqueTweet
                    {
                        PostedAt = item.PostedAt,
                        PostedBy = item.PostedBy,
                        Text = item.Text,
                        ProfileImageURL = item.ProfileImageURL,
                        TweetIdStr = item.TweetIdStr,
                        AddedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    var existingItem = entity.CliqueTweets.SingleOrDefault(res => res.TweetIdStr == request.TweetIdStr);
                    if (existingItem == null)
                    {
                        entity.CliqueTweets.Add(request);
                        entity.SaveChanges();
                        item.Id = request.Id;
                    }
                    else
                        item.Id = existingItem.Id;

                    var mappingRequest = new CliqueLocationTweet
                    {
                        TweetId = item.Id,
                        LocationId = item.LocationId,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    entity.CliqueLocationTweets.Add(mappingRequest);
                    entity.SaveChanges();
                }

            }
        }

        public bool IsLocationTweetExist(int locationId)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueLocationTweets.FirstOrDefault(res => res.LocationId == locationId);
                return response != null;
            }

        }

        public IList<EventModel> GetEvent(int requestId)
        {
            List<EventModel> response = new List<EventModel>();
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var requestEntity = entity.CliqueRequests.FirstOrDefault(res => res.Id == requestId);

                foreach (var item in requestEntity.CliqueLocation.CliqueEvents.Where(res=>(res.StartDate.Date >= requestEntity.FromDate.Date) && ((res.EndDate != null ? res.EndDate.Value.Date : res.StartDate.Date) <= requestEntity.ToDate.Date)))
                {
                    response.Add(new EventModel
                    {
                        Id = item.Id,
                        LocationId = item.LocationId,
                        Description = item.Description,
                        Name = item.Name,
                        Venue = item.Venue,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate ?? DateTime.Now,
                        Score = item.Score ?? 0
                    });

                }

            }

            return response;


        }

        public IList<TweetModel> GetLocationTweet(int requestId)
        {
            List<TweetModel> response = new List<TweetModel>();
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var requestEntity = entity.CliqueRequests.FirstOrDefault(res => res.Id == requestId);

                foreach (var item in requestEntity.CliqueLocation.CliqueLocationTweets)
                {
                    response.Add(new TweetModel
                    {
                        Id = item.Id,
                        LocationId = item.LocationId,
                       Text = item.CliqueTweet.Text,
                       PostedBy = item.CliqueTweet.PostedBy,
                       PostedAt = item.CliqueTweet.PostedAt,
                       Score = item.CliqueTweet.Score,
                       ProfileImageURL = item.CliqueTweet.ProfileImageURL
                    });

                }

            }

            return response;   
        }

        public IList<TweetModel> GetUserTweet(int requestId)
        {
            List<TweetModel> response = new List<TweetModel>();
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var requestEntity = entity.CliqueRequests.FirstOrDefault(res => res.Id == requestId);

                foreach (var item in requestEntity.CliqueUser.CliqueUserTweets)
                {
                    response.Add(new TweetModel
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        Text = item.CliqueTweet.Text,
                        PostedBy = item.CliqueTweet.PostedBy,
                        PostedAt = item.CliqueTweet.PostedAt,
                        Score = item.CliqueTweet.Score,
                        ProfileImageURL = item.CliqueTweet.ProfileImageURL
                    });

                }

            }

            return response;   
        }
    }
}

