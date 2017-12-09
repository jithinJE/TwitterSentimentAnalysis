using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Model
{
    public static class Constants
    {
        public static string AzureRequestHeaderSubscriptionKeyName = "Ocp-Apim-Subscription-Key";
        public static string AzureRequestHeaderSubscriptionKeyValue = "";


        public static string TwitterOAuthConsumerKey = "";
        public static string TwitterOAuthConsumerSecret = "";
        public static string TwitterOAuthAccessToken = "";
        public static string TwitterOAuthAccessTokenSecret = "";

        public static string TwitterSearchQuery = "";

        public static string TwitterMessageLink = "https://twitter.com/{0}/status/{1}";

        public static string DefaultTweetStatus = "Pending";

        public static string MySQLConnectionString = "server=localhost;user=root;database=twitter_sentiment_analysis;port=3306;password=root";

        public static int NoSQLPortNumber = 27017;
    }
}