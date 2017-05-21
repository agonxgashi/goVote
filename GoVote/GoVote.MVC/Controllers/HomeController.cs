using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GoVote.MVC.Infrastructure;
using GoVote.MVC.Models;
using Newtonsoft.Json;

namespace GoVote.MVC.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakeQuestion(Question question, ICollection<string> Answers)
        {
            foreach (string answer in Answers)
                question.Answers.Add(new Answer() {AnswerText = answer});

            HttpClient c = new HttpClient();
            string jSon      = JsonConvert.SerializeObject(question);
            var a            = new HttpRequestMessage(HttpMethod.Put, ApiHelper.GetApiUrl() + "vote?questionJSON=" + jSon);
            var httpResponse = c.GetStringAsync(ApiHelper.GetApiUrl() + "vote?questionJSON=" + jSon);

            while (httpResponse.Status == TaskStatus.WaitingForActivation) { }
            
            HttpCookie cookie = new HttpCookie("GoVoteQuestion", httpResponse.Result.Replace("\"", ""));
            cookie.Expires    = DateTime.MaxValue;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Question", new {questionId = httpResponse.Result.Replace("\"", "")});
        }

        public ActionResult Question(string questionId)
        {
            HttpClient c = new HttpClient();
            var a            = new HttpRequestMessage(HttpMethod.Get, ApiHelper.GetApiUrl() + "vote?uniqueId=" + questionId);
            c.Timeout        = TimeSpan.FromSeconds(10);
            var httpResponse = c.GetStringAsync(ApiHelper.GetApiUrl() + "vote?uniqueId=" + questionId);

            while (httpResponse.Status == TaskStatus.WaitingForActivation) { }

            string acceptedJSON = httpResponse.Result;
            Models.Question q   = JsonConvert.DeserializeObject<Question>(acceptedJSON);
            return View(q);
        }


        public PartialViewResult Results(int questionId)
        {
            HttpClient c = new HttpClient();
            var a = new HttpRequestMessage(HttpMethod.Get, ApiHelper.GetApiUrl() + "vote/GetAll?qId=" + questionId);
            c.Timeout = TimeSpan.FromSeconds(10);
            var httpResponse = c.GetStringAsync(ApiHelper.GetApiUrl() + "vote/GetAll?qId=" + questionId);

            while (httpResponse.Status == TaskStatus.WaitingForActivation) { }

            string acceptedJSON = httpResponse.Result ?? "[]";
            if (acceptedJSON != "[]")
            {
                List<Models.QuestionResponse> q = JsonConvert.DeserializeObject<List<QuestionResponse>>(acceptedJSON);
                return PartialView(q);
            }

            return PartialView(null);
        }

        public ActionResult Vote(int questionId, int answerId)
        {
            HttpClient c = new HttpClient();
            var a = new HttpRequestMessage(HttpMethod.Get, ApiHelper.GetApiUrl() + "vote/GetAll?qId=" + questionId);
            c.Timeout = TimeSpan.FromSeconds(10);
            string ac = ApiHelper.GetApiUrl() + "vote/RegisterVote?questionId=" + questionId + "&answerId=" + answerId;
            var httpResponse = c.GetStringAsync(ApiHelper.GetApiUrl() + "vote/RegisterVote?questionId=" + questionId + "&answerId=" + answerId);

            HttpCookie cookie = new HttpCookie("Voted", questionId.ToString());
            cookie.Expires = DateTime.MaxValue;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }

    }
}