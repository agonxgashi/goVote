using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GoVote.MVC.Models
{
    public class Question
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("UniqueId")]
        public Guid UniqueId { get; set; }

        [JsonProperty("QuestionText")]
        public string QuestionText { get; set; }

        [JsonProperty("AskedBy")]
        public int AskedBy { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("ValidUntil")]
        public DateTime ValidUntil { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("IsPublic")]
        public bool IsPublic { get; set; }

        [JsonProperty("IsMultiselect")]
        public bool IsMultiselect { get; set; }

        [JsonProperty("Answers")]
        public ICollection<Answer> Answers = new List<Answer>();

    }
}