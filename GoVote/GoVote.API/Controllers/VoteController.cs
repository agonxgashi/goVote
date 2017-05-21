﻿using System;
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

        [HttpGet]
        public string Put(string questionJSON)
        {
            string uniqueIdOfSavedQuestion = QuestionHelper.SaveQuestion(questionJSON);
            return uniqueIdOfSavedQuestion;
        }

        public Question Get(string uniqueId)
        {
            Question q = QuestionHelper.GetQuestionByUniqueId(uniqueId);
            //string jj = JsonConvert.SerializeObject(q);
            return q;
        }

        public List<QuestionResponse> GetAll(int qId)
        {
            return QuestionHelper.GetAllResponses(qId);
        }

        [HttpGet]
        public void RegisterVote(int questionId, int answerId)
        {
            QuestionHelper.RegisterVote(questionId, answerId);
        }

    }
}