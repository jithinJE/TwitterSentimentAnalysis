using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model;
using TestApp.Context;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TestApp.Data
{
    public static class NoSqlDatabase
    {
        public static void InsertSingleTweet(Tweets tweet)
        {
            IMongoCollection<Tweets> collection = new NoSQLContext().twitterCollection;

            collection.InsertOne(tweet);
        }

        public static List<Tweets> GetAllTweets(DateTime fromDate, DateTime toDate, string sentType)
        {            

            IMongoCollection<Tweets> collection = new NoSQLContext().twitterCollection;
            
            return collection.Find(b => !string.IsNullOrEmpty(b.Text) 
                                        && (  (b.sentiment.SentimentType.Equals(sentType) && !string.IsNullOrEmpty(sentType)) || string.IsNullOrEmpty(sentType)  )
                                        && (  (b.CreatedDate >= fromDate && fromDate != DateTime.MinValue) || fromDate == DateTime.MinValue)
                                        && (  (b.CreatedDate <= toDate && toDate != DateTime.MinValue) || toDate == DateTime.MinValue)
                                        ).SortByDescending(s => s.CreatedDate).ToList<Tweets>();
        }

        public static void UpdateLastTweetId(ObjectId _id, decimal _tweetId)
        {
            IMongoCollection<LastTweetInfo> collection = new NoSQLContext().lastTweetCollection;
            
            collection.ReplaceOne(b => b.TweetId != 0, new LastTweetInfo() {Id = _id,  TweetId = _tweetId });
        }

        public static void UpdateResponseStatus(decimal _tweetId, string status)
        {
            IMongoCollection<Tweets> collection = new NoSQLContext().twitterCollection;


            Tweets tweet = new Tweets();

            tweet = collection.Find(b => b.TweetId == _tweetId).ToList<Tweets>().First();
            tweet.ResponseStatus = status;

            collection = new NoSQLContext().twitterCollection;
            collection.ReplaceOne(b => b.TweetId == _tweetId, tweet);


        }

        public static LastTweetInfo GetLastTweetId()
        {
            IMongoCollection<LastTweetInfo> collection = new NoSQLContext().lastTweetCollection;

            return collection.Find(b => b.TweetId != 0).First<LastTweetInfo>();            
        }
    }
}
