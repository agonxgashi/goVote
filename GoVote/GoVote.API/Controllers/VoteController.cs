using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoVote.API.Methods;
using GoVote.MVC.Models;
using Newtonsoft.Json;

namespace GoVote.API.Controllers
{
    public class VoteController : ApiController
    {
        IQuestionHelper remoteObject;

        public VoteController() {
            remoteObject = new QuestionHelper();
        }
        [HttpGet]
        public string Put(string questionJSON)
        {
            return remoteObject.SaveQuestion(questionJSON);
        }

        public Question Get(string uniqueId)
        {
            return remoteObject.GetQuestionByUniqueId(uniqueId);
        }

        public List<QuestionResponse> GetAll(int qId)
        {
            return remoteObject.GetAllResponses(qId);
        }

        [HttpGet]
        public void RegisterVote(int questionId, int answerId)
        {
            remoteObject.RegisterVote(questionId, answerId);
        }

    }
}
