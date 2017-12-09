using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TestApp.Model
{
    public class LastTweetInfo
    {
        public ObjectId Id;
        public decimal TweetId { get; set; }
    }
}
