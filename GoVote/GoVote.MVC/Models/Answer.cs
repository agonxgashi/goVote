using Newtonsoft.Json;

namespace GoVote.MVC.Models
{
    public class Answer
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("QuestionId")]
        public int QuestionId { get; set; }

        [JsonProperty("AnswerText")]
        public string AnswerText { get; set; }

    }
}