using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model;
using TestApp.Data;
using Twitterizer;

namespace TestApp.API
{
    public static class TwitterAPI
    {
        public static List<Tweets> GetTweetList()
        {
            LastTweetInfo lastTweetInfo = NoSqlDatabase.GetLastTweetId();

            decimal TweetIdBefore = lastTweetInfo.TweetId;
            decimal TweetIdAfter = 0;


            OAuthTokens tokens = new OAuthTokens();
            tokens.ConsumerKey = Constants.TwitterOAuthConsumerKey;
            tokens.ConsumerSecret = Constants.TwitterOAuthConsumerSecret;
            tokens.AccessToken = Constants.TwitterOAuthAccessToken;
            tokens.AccessTokenSecret = Constants.TwitterOAuthAccessTokenSecret;

            SearchOptions options = new SearchOptions();

            options.SinceId = TweetIdBefore;
            //options.SinceId = 932808858153639937;

            options.ResultType = SearchOptionsResultType.Recent;

            TwitterResponse<TwitterSearchResultCollection> result = TwitterSearch.Search(tokens, Constants.TwitterSearchQuery, options);


            decimal userid = 0;
            List<Tweets> resultTweets = new List<Tweets>();

            foreach (var myresult in result.ResponseObject)
            {
                
                userid = myresult.User.Id;
                double sentimentScore = AzureAPI.getSentimentScore(myresult.Text);

                string tweetLink = string.Format(Constants.TwitterMessageLink, myresult.User.ScreenName, myresult.StringId);
                
                resultTweets.Add(TwitterAPI.BuildTweet(myresult.Id, tweetLink, myresult.CreatedDate, myresult.Text, sentimentScore, Constants.DefaultTweetStatus));
            }

            //TweetIdAfter = result.ResponseObject.OrderByDescending(tweets => tweets.Id).First().Id;
            TweetIdAfter = resultTweets.Count == 0 ? 0 : resultTweets.OrderByDescending(tweets => tweets.TweetId).First().TweetId;

            if (TweetIdAfter != 0)
            {
               
                NoSqlDatabase.UpdateLastTweetId(lastTweetInfo.Id, TweetIdAfter);
            }

            return resultTweets;
        }

        public static Tweets BuildTweet(decimal _tweetId, string _tweetLink, DateTime _createdDateTime, string _text, double _sentimentScore, string _responseStatus)
        {
            string sentiment = string.Empty;
            if (_sentimentScore >= 0.6)
            {
                sentiment = "Positive";
            }
            else if (_sentimentScore <= 0.4)
            {
                sentiment = "Negative";
            }
            else
            {
                sentiment = "Neutral";
            }

            return new Tweets()
            {
                TweetId = _tweetId,
                CreatedDate = _createdDateTime,
                Text = _text,
                sentiment = new Sentiment()
                {
                    Score = _sentimentScore,
                    SentimentType = sentiment
                },
                ResponseStatus = _responseStatus,
                TweetLink = _tweetLink
            };
        }
    }
}
