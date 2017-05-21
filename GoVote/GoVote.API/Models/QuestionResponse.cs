namespace GoVote.MVC.Models
{
    public class QuestionResponse
    {
        public string QuestionText { get; set; }
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int Count { get; set; }

    }
}