using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using UserProfilerCommon;
using UserProfilerWeb.Models;

namespace UserProfilerWeb.Controllers
{
    public class TwitterAPIController : ApiController
    {
        private ipl_userprofilerEntities db = new ipl_userprofilerEntities();

        // GET: api/TwitterAPI
        public IList<TwitterUserModel> GetTwitterUsers()
        {
            var response = new List<TwitterUserModel>();

            foreach (var item in db.TwitterUsers)
            {
                response.Add(new TwitterUserModel { Id = item.Id, Name = item.Name, UserName = item.UserName, ImageURL = item.ImageURL });
            }
           
            return response;
        }

        // GET: api/TwitterAPI/5
        [ResponseType(typeof(TwitterUser))]
        public async Task<IHttpActionResult> GetTwitterUser(int id)
        {
            TwitterUser twitterUser = await db.TwitterUsers.FindAsync(id);
            if (twitterUser == null)
            {
                return NotFound();
            }

            return Ok(twitterUser);
        }

        // PUT: api/TwitterAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTwitterUser(int id, TwitterUser twitterUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != twitterUser.Id)
            {
                return BadRequest();
            }

            db.Entry(twitterUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TwitterUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TwitterAPI
        [ResponseType(typeof(TwitterUserModel))]
        public async Task<IHttpActionResult> PostTwitterUser(TwitterUserModel twitterUser)
        {
            var newUser = new TwitterUser
            {
                Name = twitterUser.Name,
                Description = "test",
                ImageURL = "added",
                AddedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                FollowersCount = 1,
                FollowingCount = 1,
                UserName = twitterUser.UserName
            };
            db.TwitterUsers.Add(
               newUser
            );
            await db.SaveChangesAsync();

            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue thumbnailRequestQueue = queueClient.GetQueueReference("addrequest");
            thumbnailRequestQueue.CreateIfNotExists();
            var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(new NewTwitterAddInformation { UserId = newUser.Id }));
            await thumbnailRequestQueue.AddMessageAsync(queueMessage);

            return CreatedAtRoute("DefaultApi", new { id = newUser.Id }, twitterUser);
        }

        // DELETE: api/TwitterAPI/5
        [ResponseType(typeof(TwitterUser))]
        public async Task<IHttpActionResult> DeleteTwitterUser(int id)
        {
            TwitterUser twitterUser = await db.TwitterUsers.FindAsync(id);
            if (twitterUser == null)
            {
                return NotFound();
            }

            db.TwitterUsers.Remove(twitterUser);
            await db.SaveChangesAsync();

            return Ok(twitterUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TwitterUserExists(int id)
        {
            return db.TwitterUsers.Count(e => e.Id == id) > 0;
        }
    }
}