using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TestApp.Model;
using TestApp.Data;
using TestApp.API;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Welcomee()
        {
            string param1 = this.Request.QueryString["username"];
            string param2 = this.Request.QueryString["password"];
            if(param1 == "admin" && param2 == "admin")
            {
                List<Tweets> myTweets = NoSqlDatabase.GetAllTweets(DateTime.MinValue, DateTime.MinValue, null);

                ViewBag.MyTweets = myTweets;

               


                return View("Tweets"); 
            }
           else
            {
                ViewBag.Message = "wrong password";
                return View("login1");
            }
        }
        //[HttpPost]
       // public ActionResult Index(Login login)
        //{
          //  return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult HelloWorld()
        {
            List<Tweets> tweetList = TwitterAPI.GetTweetList();

            foreach (Tweets tweet in tweetList)
            {
                NoSqlDatabase.InsertSingleTweet(tweet);
            }
            ViewBag.status = "Success";
            return View("Contact");
        }


        public class Log
        {
            public int username;
            public string password;
        }

       public ActionResult Tweets()
        {
            string fromDateParam = this.Request.QueryString["startdate"];
            string toDateParam = this.Request.QueryString["enddate"];
            string sentTypeParam = this.Request.QueryString["sentitype"];

            DateTime fromDate = string.IsNullOrEmpty(fromDateParam) ? DateTime.MinValue : Convert.ToDateTime(string.Format("{0} 12:00:00 AM", fromDateParam));
            DateTime toDate = string.IsNullOrEmpty(toDateParam) ? DateTime.MinValue : Convert.ToDateTime(string.Format("{0} 11:59:59 PM", toDateParam));

            List<Tweets> myTweets = NoSqlDatabase.GetAllTweets(fromDate, toDate, sentTypeParam);

            ViewBag.MyTweets = myTweets;

            

            ViewBag.Message = "Jithin";

            return View();
        }



        [HttpGet]
        public ActionResult Login()
        {
            

            string tweeid = this.Request.QueryString["Tweetid"];
            ViewBag.tweetid = tweeid;
            
            

            

            return View();
        }

        public ActionResult Login2()
        {
            int st = 0;
            string tid = this.Request.QueryString["ticketid"];
            string ttype = this.Request.QueryString["ttype"];            
            string assignedto = this.Request.QueryString["assignedto"];
            string tdetails = this.Request.QueryString["tdetails"];
            string tweeid = this.Request.QueryString["tweetid"];
            string status = this.Request.QueryString["status"];
            int tickid = Int32.Parse(tid);
            string tdate = DateTime.Now.Date.ToString();//this.Request.QueryString["tdate"];


            if (!String.IsNullOrEmpty(ttype))
            {
                st = MySqlDatabase.InsertTicket(tickid,ttype,tweeid,tdetails,tdate,assignedto,status);
            }

            if(st==1)
            {
                NoSqlDatabase.UpdateResponseStatus(Decimal.Parse(tweeid), status);
            }

            ViewBag.Tweetid = tweeid;

            List<Tweets> myTweets = NoSqlDatabase.GetAllTweets(DateTime.MinValue, DateTime.MinValue, null);

            ViewBag.MyTweets = myTweets;


            return View("Tweets");
        }
        public ActionResult login1()
        {
            string s = ViewBag.username;


            return View();
        }
        public ActionResult NEW()
        {
            string s = ViewBag.username;


            return View();
        }
    }
}