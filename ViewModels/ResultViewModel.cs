using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace TestApplication.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ResultViewModel
    {
        //region constructor
        public ResultViewModel()
        {
        }

        //proterties
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Text { get; set; }
        [DefaultValue(0)]
        public int Type { get; set; }
        [DefaultValue(0)]
        public int Flags { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
