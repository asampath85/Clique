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
                        Locality = item.CliqueLocation.Locality,
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
                        EventId = item.EventId,
                        Name = item.Name,
                        Description = item.Description,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Venue = item.Venue,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    var existingItem = entity.CliqueEvents.FirstOrDefault(res=>res.EventId == request.EventId);
                    if(existingItem == null)
                    {
                        entity.CliqueEvents.Add(request);
                        entity.SaveChanges();
                        item.Id = request.Id;
                    }
                    else
                    {
                        item.Id = existingItem.Id;
                    }

                    var mappingRequest = new CliqueLocationEvent
                    {
                        EventId = item.Id,
                        RequestId = item.RequestId,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    entity.CliqueLocationEvents.Add(mappingRequest);
                    entity.SaveChanges();
                    

                    


                }

                


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

        public bool IsEventExist(RequestModel model)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueLocationEvents.FirstOrDefault(res => res.RequestId == model.Id);
                return response != null;
            }
        }       

        public bool IsUserTweetExist(RequestModel model)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueUserTweets.FirstOrDefault(res => res.RequestId == model.Id);
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
                        RequestId = item.RequestId,
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
                        ModifiedAt = DateTime.Now,
                        Score = item.Score
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
                        RequestId = item.RequestId,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now
                    };
                    entity.CliqueLocationTweets.Add(mappingRequest);
                    entity.SaveChanges();
                }

            }
        }

        public bool IsLocationTweetExist(RequestModel model)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueLocationTweets.FirstOrDefault(res => res.RequestId == model.Id);
                return response != null;
            }

        }

        public IList<EventModel> GetEvent(int requestId)
        {
            List<EventModel> response = new List<EventModel>();
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var requestEntity = entity.CliqueRequests.FirstOrDefault(res => res.Id == requestId);
                foreach (var events in requestEntity.CliqueLocationEvents)
                {
                    if((events.CliqueEvent.StartDate.Date >= requestEntity.FromDate.Date) && ((events.CliqueEvent.EndDate != null ? events.CliqueEvent.EndDate.Value.Date : events.CliqueEvent.StartDate.Date) <= requestEntity.ToDate.Date))
                        response.Add(new EventModel
                        {
                            Id = events.CliqueEvent.Id,
                            EventId = events.CliqueEvent.EventId,
                            Description = events.CliqueEvent.Description,
                            Name = events.CliqueEvent.Name,
                            Venue = events.CliqueEvent.Venue,
                            StartDate = events.CliqueEvent.StartDate,
                            EndDate = events.CliqueEvent.EndDate ?? DateTime.Now,
                            Score = events.CliqueEvent.Score ?? 0
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

                foreach (var item in requestEntity.CliqueLocationTweets)
                {
                    response.Add(new TweetModel
                    {
                        Id = item.CliqueTweet.Id,
                        RequestId = requestEntity.Id,
                       Text = item.CliqueTweet.Text,
                       PostedBy = item.CliqueTweet.PostedBy,
                       PostedAt = item.CliqueTweet.PostedAt,
                       Score = item.CliqueTweet.Score??0,
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

                foreach (var item in requestEntity.CliqueUserTweets)
                {
                    response.Add(new TweetModel
                    {
                        Id = item.CliqueTweet.Id,
                        RequestId = requestEntity.Id,
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

        public bool AddProperty(HomeAwayPropertyModel model)
        {
            var request = new HomeAwayProperty
            {
               Accomodates = model.Accomodates,
               Address1 = model.Address1,
               Address2 = model.Address2,
               Bedrooms = model.Bedrooms,
               Beds = model.Beds,
               City = model.City,
               Country = model.Country,
               Description = model.Description,
               Locality = model.City,
               Name = model.Name,
               NightPrice = model.NightPrice,
               State = model.State,
               Type = model.Type,
               WeekPrice = model.WeekPrice,
               Zip = model.Zip,
               AddedAt = DateTime.Now
            };

            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                entity.HomeAwayProperties.Add(request);
                entity.SaveChanges();

            }
            
            return true;
        }

        public IList<HomeAwayPropertyModel> GetProperty(int id = 0)
        {
            List<HomeAwayPropertyModel> response = new List<HomeAwayPropertyModel>();
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                foreach (var item in entity.HomeAwayProperties.Where(res=>(res.Id == id || id == 0)))
                {
                    response.Add(new HomeAwayPropertyModel
                    {
                        Id = item.Id,
                        Accomodates = item.Accomodates,
                        AddedAt = item.AddedAt,
                        Address1 = item.Address1,
                        Address2 = item.Address2,
                        Bedrooms = item.Bedrooms,
                        Beds = item.Beds,
                        City = item.City,
                        Country = item.Country,
                        Description = item.Description,
                        Locality = item.Locality,
                        Name = item.Name,
                        NightPrice = item.NightPrice??0,
                        State = item.State,
                        Type = item.Type,
                        Zip = item.Zip,
                        WeekPrice = item.WeekPrice??0
                        
                    });

                }

            }

            return response;  
        }
    }
}

