using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using GoVote.MVC.Infrastructure;

namespace GoVote.MVC.Controllers
{
    public class TestAPIController : Controller
    {
        // GET: TestAPI
        public ActionResult Index()
        {

            HttpClient c = new HttpClient();
            ViewBag.Res = c.GetStringAsync( ApiHelper.GetApiUrl() + "ATest").Result;

            return View();
        }
    }
}