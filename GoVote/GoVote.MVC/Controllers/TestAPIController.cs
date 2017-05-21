using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using GoVote.MVC.Infrastructure;
using GoVote.MVC.Models;

namespace GoVote.MVC.Controllers
{
    public class TestAPIController : Controller
    {
        // GET: TestAPI
        public ActionResult Index(Question question, ICollection<string> Answers )
        {

            HttpClient c = new HttpClient();
            //ViewBag.Res = c.GetStringAsync( ApiHelper.GetApiUrl() + "ATest").Result;

            return View();
        }
    }
}