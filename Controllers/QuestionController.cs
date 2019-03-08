using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApplication.ViewModels;
using Newtonsoft.Json;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApplication.Controllers
{
    //specify that that the class can be used as an Api
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<QuestionViewModel>();

            sampleQuestions.Add(new QuestionViewModel()
            {
                Id = 1,
                QuizId = quizId,
                Text = "What do you value most in your life?",
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            //add a bunch of other sample questions
            for(int i= 2; i <= 5; i++)
            {
                sampleQuestions.Add(
                    new QuestionViewModel() { 
                        Id = i,
                        QuizId = quizId,
                        Text = String.Format("Sample Question {0}", i),
                        CreateDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now
                    }
                );
            }
            return new JsonResult(
                sampleQuestions, new JsonSerializerSettings(){ Formatting = Formatting.Indented}
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Content("Not implemented (yet)!");
        }
        [HttpPost]
        public IActionResult Post(QuestionViewModel m)
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        public IActionResult Put(QuestionViewModel m)
        {
            throw new NotImplementedException();
        }
    }
}
