using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestApplication.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        [HttpGet("All/{questionId}")]
        public IActionResult All( int questionId)
        {
            var sampleAnswers = new List<AnswerViewModel>();

            //Add the first kind of data.
            sampleAnswers.Add(new AnswerViewModel()
            {
                Id = 1,
                QuestionId = questionId,
                Text = "Friends and family",
                CreateDate = DateTime.Now,
                LastModified = DateTime.Now
            });

            for(int i = 2; i <= 5; i++)
            {
                sampleAnswers.Add(new AnswerViewModel() {
                    Id = i,
                    QuestionId = questionId,
                    Text = String.Format("Sample Answer {0}", i),
                    CreateDate = DateTime.Now,
                    LastModified = DateTime.Now
                });
            }
            return new JsonResult(sampleAnswers, new JsonSerializerSettings() { Formatting = Formatting.Indented});
        }
    }
}
