using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TestApplication.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class QuestionViewModel
    {
        //region constructor
        public QuestionViewModel()
        {
        }

        //properties
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        [DefaultValue(0)]
        public int Type { get; set; }
        [DefaultValue(0)]
        public int Flages { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
