using GoVote.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoVote.API.Methods
{
    public interface IQuestionHelper
    {
        string SaveQuestion(string question);
        void SaveAnswers(ICollection<Answer> Answer, string questrionId);
        Question GetQuestionByUniqueId(string uniqueId);
        List<QuestionResponse> GetAllResponses(int id);
        void RegisterVote(int questionId, int answerId);
    }
}