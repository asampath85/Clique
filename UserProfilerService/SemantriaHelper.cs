using Semantria.Com;
using Semantria.Com.Mapping;
using Semantria.Com.Mapping.Output;
using Semantria.Com.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfilerModel;

namespace UserProfilerService
{


    public class SemantriaHelper
    {

        const string consumerKey = "dc3a2cbc-e4ae-4675-9626-1b37e526bb26";
        const string consumerSecret = "dba1bc59-77ab-41b2-add0-a0809feb2416";

        public static void AddUserFeedbackScore(UserFeedbackModel model)
        {
            // Creates JSON serializer instance
            ISerializer serializer = new JsonSerializer();

            // Initializes new session with the serializer object and the keys.
            using (Session session = Session.CreateSession(consumerKey, consumerSecret, serializer))
            {
                // Error callback handler. This event will occur in case of server-side error
                session.Error += new Session.ErrorHandler(delegate(object sender, ResponseErrorEventArgs ea)
                {
                    //Console.WriteLine(string.Format("{0}: {1}", (int)ea.Status, ea.Message));
                });

                //Obtaining subscription object to get user limits applicable on server side
                Subscription subscription = session.GetSubscription();
                Dictionary<string, Semantria.Com.TaskStatus> docsTracker = new Dictionary<string, Semantria.Com.TaskStatus>();

                List<Document> outgoingBatch = new List<Document>(subscription.BasicSettings.BatchLimit);

                string docId = Guid.NewGuid().ToString();

                Document doc = new Document()
                {
                    Id = docId,
                    Text = model.Text
                };

                    outgoingBatch.Add(doc);
                    docsTracker.Add(docId, Semantria.Com.TaskStatus.QUEUED);

                                

                if (outgoingBatch.Count > 0)
                {
                    // Queues batch of documents for processing on Semantria service
                    if (session.QueueBatchOfDocuments(outgoingBatch) != -1)
                    {
                        Console.WriteLine(string.Format("{0} documents queued successfully.", outgoingBatch.Count));
                    }
                }

                List<DocAnalyticData> results = new List<DocAnalyticData>();
                while (docsTracker.Any(item => item.Value == Semantria.Com.TaskStatus.QUEUED))
                {
                    System.Threading.Thread.Sleep(500);

                    // Requests processed results from Semantria service
                    Console.WriteLine("Retrieving your processed results...");
                    IList<DocAnalyticData> incomingBatch = session.GetProcessedDocuments();

                    foreach (DocAnalyticData item in incomingBatch)
                    {
                        if (docsTracker.ContainsKey(item.Id))
                        {
                            docsTracker[item.Id] = item.Status;
                            results.Add(item);
                        }
                    }
                }


                foreach (DocAnalyticData data in results)
                {
                     float score = 0;
                    // Printing of document entities
                    if (data.Phrases != null && data.Phrases.Count > 0)
                    {

                        foreach (var entity in data.Phrases)
                        {
                            score += entity.SentimentScore;
                        }
                        score = score / data.Phrases.Count();
                    }

                    model.Score = score;

                }
            }

        }
    }
}
