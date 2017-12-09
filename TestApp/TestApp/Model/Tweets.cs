using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TestApp.Model
{
    public class Tweets
    {
        public ObjectId Id { get; set; }
        public decimal TweetId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Text { get; set; }
        public Sentiment sentiment { get; set; }        
        public string ResponseStatus { get; set; }
        public string TweetLink { get; set; }
    }
}
