using System;
using System.Collections.Generic;
using GoVote.MVC.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace GoVote.API.Methods
{
    public class QuestionHelper
    {
        public static string SaveQuestion(string question)
        {
            string qId = String.Empty;
            Question q = JsonConvert.DeserializeObject<Question>(question);

            using (SqlConnection con = new SqlConnection(DBHelper.GetConStr()))
            {
                SqlCommand com = new SqlCommand()
                {
                    CommandText = "Vote.usp_CreateQuestion",
                    CommandType = CommandType.StoredProcedure,
                    Connection  = con
                };
                com.Parameters.AddWithValue("@param_QuestionText" , q.QuestionText);
                com.Parameters.AddWithValue("@param_ValidUntil"   , DateTime.Now.AddMinutes(10));
                com.Parameters.AddWithValue("@param_IsPublic"     , q.IsPublic);
                com.Parameters.AddWithValue("@param_IsMultiselect", q.IsMultiselect);

                try
                {
                    con.Open();
                    var reader = com.ExecuteReader();
                    while (reader.Read())
                        qId = reader[0].ToString();

                    SaveAnswers(q.Answers, qId);
                    return qId;
                }
                catch (Exception) { }
            }
            return "";
        }


        private static void SaveAnswers(ICollection<Answer> Answer, string questrionId)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetConStr()))
            {
                SqlCommand com = new SqlCommand()
                {
                    CommandText = "Vote.usp_CreateAnswer",
                    CommandType = CommandType.StoredProcedure,
                    Connection  = con
                };

                con.Open();
                foreach (var a in Answer)
                {
                    if (!String.IsNullOrEmpty(a.AnswerText))
                    {
                        com.Parameters.Clear();
                        com.Parameters.AddWithValue("@param_QuestionId", questrionId);
                        com.Parameters.AddWithValue("@param_AnswerText", a.AnswerText);
                        try
                        {
                            com.ExecuteNonQuery();
                        }
                        catch (Exception) { }
                    }
                }
            }
        }

        public static Question GetQuestionByUniqueId(string uniqueId)
        {
            Question q            = new Question();
            bool isQuestionFilled = false;
            using (SqlConnection con = new SqlConnection(DBHelper.GetConStr()))
            {
                SqlCommand com = new SqlCommand()
                {
                    CommandText = "Vote.usp_getQuestionByUniqueId",
                    CommandType = CommandType.StoredProcedure,
                    Connection  = con
                };                

                try
                {
                    com.Parameters.AddWithValue("@param_UniqueId", Guid.Parse(uniqueId));
                    con.Open();
                    var reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!isQuestionFilled)
                        {
                            q.Id             = int.Parse(reader["Id"].ToString());
                            q.UniqueId       = Guid.Parse(reader["UniqueId"].ToString());
                            q.QuestionText   = reader["QuestionText"].ToString();
                            q.AskedBy        = !String.IsNullOrEmpty(reader["AskedBy"].ToString()) ? int.Parse(reader["AskedBy"].ToString()) : 0;
                            q.IsMultiselect  = bool.Parse(reader["IsMultiselect"].ToString());
                            q.DateCreated    = DateTime.Parse(reader["DateCreated"].ToString());
                            q.ValidUntil     = DateTime.Parse(reader["ValidUntil"].ToString());
                            q.IsActive       = bool.Parse(reader["IsActive"].ToString());
                            q.IsPublic       = bool.Parse(reader["IsPublic"].ToString());
                            isQuestionFilled = true;
                        }
                        q.Answers.Add(new Answer()
                        {
                            Id         = int.Parse(reader["AnswerId"].ToString()),
                            AnswerText = reader["AnswerText"].ToString()
                        });
                    }
                }
                catch (Exception) { }
            }
            return q;

        }

        public static List<QuestionResponse> GetAllResponses(int id)
        {
            List<QuestionResponse> res = new List<QuestionResponse>();
            using (SqlConnection con = new SqlConnection(DBHelper.GetConStr()))
            {
                SqlCommand com = new SqlCommand()
                {
                    CommandText = "Vote.usp_GetVotesOfQuestion",
                    CommandType = CommandType.StoredProcedure,
                    Connection = con
                };
                com.Parameters.AddWithValue("@param_QuestionId", id);

                try
                {
                    con.Open();
                    var reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        res.Add(new QuestionResponse()
                        {
                            QuestionText = reader["QuestionText"].ToString(),
                            AnswerText = reader["AnswerText"].ToString(),
                            AnswerId = int.Parse(reader["AnswerId"].ToString()),
                            Count = int.Parse(reader["Count"].ToString())
                        });
                    }
                }
                catch (Exception e)
                {

                }
            }
            return res;
        }

        public static void RegisterVote(int questionId, int answerId)
        {
            using (SqlConnection con = new SqlConnection(DBHelper.GetConStr()))
            {
                SqlCommand com = new SqlCommand()
                {
                    CommandText = "Vote.usp_RegisterResponse",
                    CommandType = CommandType.StoredProcedure,
                    Connection  = con
                };
                try
                {
                    con.Open();
                    com.Parameters.AddWithValue("@param_QuestionId", questionId);
                    com.Parameters.AddWithValue("@param_AnswerId", answerId);
                    com.ExecuteNonQuery();
                }
                catch (Exception) { }
            }
        }

    }
}