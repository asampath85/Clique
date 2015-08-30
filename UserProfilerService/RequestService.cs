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
                    else if (item.Status == 2)
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
                var response = entity.CliqueClaimRequests.SingleOrDefault(res => res.Id == requestId);
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
                    var existingItem = entity.CliqueEvents.FirstOrDefault(res => res.EventId == request.EventId);
                    if (existingItem == null)
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

        public bool IsEventExist(CliqueClaimRequestModel model)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueLocationEvents.Where(res => res.RequestId == model.Id);
                entity.CliqueLocationEvents.RemoveRange(response);
                entity.SaveChanges();
            }
            return false;
        }

        public bool IsUserTweetExist(CliqueClaimRequestModel model)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueUserTweets.Where(res => res.RequestId == model.Id);
                entity.CliqueUserTweets.RemoveRange(response);
                entity.SaveChanges();
            }
            return false;

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

        public bool IsLocationTweetExist(CliqueClaimRequestModel model)
        {
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var response = entity.CliqueLocationTweets.Where(res => res.RequestId == model.Id);
                entity.CliqueLocationTweets.RemoveRange(response);
                entity.SaveChanges();                
            }
            return false;

        }

        public IList<EventModel> GetEvent(int requestId)
        {
            List<EventModel> response = new List<EventModel>();
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var requestEntity = entity.CliqueClaimRequests.FirstOrDefault(res => res.Id == requestId);
                foreach (var events in requestEntity.CliqueLocationEvents)
                {
                    if ((events.CliqueEvent.StartDate.Date >= requestEntity.FromDate.Date) && ((events.CliqueEvent.EndDate != null ? events.CliqueEvent.EndDate.Value.Date : events.CliqueEvent.StartDate.Date) <= requestEntity.ToDate.Date))
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
                var requestEntity = entity.CliqueClaimRequests.FirstOrDefault(res => res.Id == requestId);

                foreach (var item in requestEntity.CliqueLocationTweets)
                {
                    response.Add(new TweetModel
                    {
                        Id = item.CliqueTweet.Id,
                        RequestId = requestEntity.Id,
                        Text = item.CliqueTweet.Text,
                        PostedBy = item.CliqueTweet.PostedBy,
                        PostedAt = item.CliqueTweet.PostedAt,
                        Score = item.CliqueTweet.Score ?? 0,
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
            HomeAwayProperty request = null;
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                if (model.Id == 0)
                {
                    request = new HomeAwayProperty();
                    entity.HomeAwayProperties.Add(request);
                    request.AddedAt = DateTime.Now;
                }
                else
                {
                    request = entity.HomeAwayProperties.FirstOrDefault(res => res.Id == model.Id);
                }


                request.Accomodates = model.Accomodates;
                request.Address1 = model.Address1;
                request.Address2 = model.Address2;
                request.Bedrooms = model.Bedrooms;
                request.Beds = model.Beds;
                request.City = model.City;
                request.Country = model.Country;
                request.Description = model.Description;
                request.Locality = model.City;
                request.Name = model.Name;
                request.NightPrice = model.NightPrice;
                request.State = model.State;
                request.Type = model.Type;
                request.WeekPrice = model.WeekPrice;
                request.Zip = model.Zip;

                entity.SaveChanges();

            }

            return true;
        }

        public CliqueClaimRequestModel AddClaimRequest(CliqueClaimRequestModel model)
        {
            CliqueClaimRequest request = null;
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                if (model.Id == 0)
                {
                    request = new CliqueClaimRequest();
                    entity.CliqueClaimRequests.Add(request);
                    request.AddedAt = DateTime.Now;
                }
                else
                {
                    request = entity.CliqueClaimRequests.FirstOrDefault(res => res.Id == model.Id);
                }


                request.Accomodates = model.Accomodates;
                request.Address1 = model.Address1;
                request.Address2 = model.Address2;
                request.Bedrooms = model.Bedrooms;
                request.Beds = model.Beds;
                request.City = model.City;
                request.Country = model.Country;
                request.Description = model.Description;
                request.Locality = model.City;
                request.Name = model.Name;
                request.NightPrice = model.NightPrice;
                request.State = model.State;
                request.Type = model.Type;
                request.WeekPrice = model.WeekPrice;
                request.Zip = model.Zip;
                request.SSN = model.SSN;
                request.IsACAvailable = model.IsACAvailable;
                request.IsBuzzerAvailable = model.IsBuzzerAvailable;
                request.IsLiftAvailable = model.IsLiftAvailable;
                request.IsPetsAllowed = model.IsPetsAllowed;
                request.IsPrivatePoolAvailable = model.IsPrivatePoolAvailable;
                request.IsWifiAvailable = model.IsWifiAvailable;
                request.FromDate = model.FromDate;
                request.ToDate = model.ToDate;
                request.Price = model.Price;
                request.Score = model.Score;
                request.Status = model.Status;
                request.Latitude = model.Latitude;
                request.Longitude = model.Longitude;

                entity.SaveChanges();
                model.Id = request.Id;
            }

            return model;
        }

        public IList<CliqueClaimRequestModel> GetClaimRequest(int id = 0)
        {
            List<CliqueClaimRequestModel> response = new List<CliqueClaimRequestModel>();
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                foreach (var item in entity.CliqueClaimRequests.Where(res => (res.Id == id || id == 0)))
                {

                    string statusName = "";
                    if ((item.Status ?? 0) == 0)
                        statusName = "New";
                    else if (item.Status == 1)
                        statusName = "Queued";
                    else if (item.Status == 2)
                        statusName = "Processed";
                    else
                        statusName = "Error";

                    response.Add(new CliqueClaimRequestModel
                    {
                        Id = item.Id,
                        Accomodates = item.Accomodates,
                        AddedAt = item.AddedAt??DateTime.Now,
                        Address1 = item.Address1,
                        Address2 = item.Address2,
                        Bedrooms = item.Bedrooms,
                        Beds = item.Beds,
                        City = item.City,
                        Country = item.Country,
                        Description = item.Description,
                        Locality = item.Locality,
                        Name = item.Name,
                        NightPrice = item.NightPrice ?? 0,
                        State = item.State,
                        Type = item.Type,
                        Zip = item.Zip,
                        WeekPrice = item.WeekPrice ?? 0,
                        SSN = item.SSN,
                        IsACAvailable = item.IsACAvailable??false,
                        IsBuzzerAvailable = item.IsBuzzerAvailable??false,
                        IsLiftAvailable = item.IsLiftAvailable??false,
                        IsPetsAllowed = item.IsPetsAllowed??false,
                        IsPrivatePoolAvailable = item.IsPrivatePoolAvailable??false,
                        IsWifiAvailable = item.IsWifiAvailable??false,
                        Score = item.Score??0,
                        Price = item.Price??0,
                        Status = item.Status??0,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        StatusName = statusName,
                        Latitude = item.Latitude??0,
                        Longitude = item.Longitude??0

                    });

                }

            }

            return response;
        }

        public IList<HomeAwayPropertyModel> GetProperty(int id = 0)
        {
            List<HomeAwayPropertyModel> response = new List<HomeAwayPropertyModel>();
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                foreach (var item in entity.HomeAwayProperties.Where(res => (res.Id == id || id == 0)))
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
                        NightPrice = item.NightPrice ?? 0,
                        State = item.State,
                        Type = item.Type,
                        Zip = item.Zip,
                        WeekPrice = item.WeekPrice ?? 0

                    });

                }

            }

            return response;
        }

        public void AddUserFeedback(UserFeedbackModel model)
        {
            SemantriaHelper.AddUserFeedbackScore(model);

            model.BuildingName = "Ahobilam";
            model.ZipCode = "600063";
            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                var cliqueRequest = entity.CliqueClaimRequests.Where(x => (x.Name == model.BuildingName && x.Zip == model.ZipCode));
                model.id = cliqueRequest.First().Id;
            }
            var request = new CliqueClaimRequestFeedback
            {
                RequestId = model.id,
                UserEmail = model.EmailId,
                UserName = model.Name,
                Text = model.Feedback,
                Score=model.Score,
                AddedAt=DateTime.Now

            };

            using (ipl_userprofilerEntities entity = new ipl_userprofilerEntities())
            {
                entity.CliqueClaimRequestFeedbacks.Add(request);
                entity.SaveChanges();
            }


        }
    }
}

