using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TestApplication.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class AnswerViewModel
    {
        //region constructor
        public AnswerViewModel()
        {
        }

        //properties
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        [DefaultValue(0)]
        public int Type { get; set; }
        [DefaultValue(0)]
        public int Flags { get; set; }
        [DefaultValue(0)]
        public int Value { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        public DateTime LastModified { get; set; }

    }
}
