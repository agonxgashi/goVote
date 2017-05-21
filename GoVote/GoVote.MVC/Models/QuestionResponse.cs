using Newtonsoft.Json;

namespace GoVote.MVC.Models
{
    public class QuestionResponse
    {
        [JsonProperty("QuestionText")]
        public string QuestionText { get; set; }

        [JsonProperty("AnswerId")]
        public int AnswerId { get; set; }

        [JsonProperty("AnswerText")]
        public string AnswerText { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

    }
}