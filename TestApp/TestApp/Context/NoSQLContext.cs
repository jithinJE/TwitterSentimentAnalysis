using TestApp.Model;
using MongoDB.Driver;


namespace TestApp.Context
{
    public class NoSQLContext
    {
        public IMongoCollection<Tweets> twitterCollection;
        public IMongoCollection<LastTweetInfo> lastTweetCollection;

        public NoSQLContext()
        {
            MongoClientSettings clientSettings = new MongoClientSettings();
            clientSettings.Server = new MongoServerAddress("localhost", Constants.NoSQLPortNumber);
            MongoClient mongoClient = new MongoClient(clientSettings);
            var db = mongoClient.GetDatabase("twitter_sentiment_analysis");
            twitterCollection = db.GetCollection<Tweets>("TwitterData");
            lastTweetCollection = db.GetCollection<LastTweetInfo>("LastTweetInfo");
        }
    }
}
