using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestApplication.ViewModels;
using System.Collections.Generic;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {

        //Get api /quiz method
        [HttpGet("Latest/{num}")]
        public IActionResult Latest(int num = 10)
        {
            //create a list of possible quizzes
            var sampleQuizzes = new List<QuizViewModel>();

            //add the first sample quiz
            sampleQuizzes.Add(new QuizViewModel()
            {
                Id = 1,
                Title = "Which character are you?",
                Description = "The best character of all time",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            //add a bunch of other sample quizzes
            for (int i = 2; i <= num; i++)
            {
                sampleQuizzes.Add(new QuizViewModel()
                {
                    Id = i,
                    Title = String.Format("Sample Quiz {0}", i),
                    Description = "This is a sample quiz",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }
            return new JsonResult(
                sampleQuizzes, new JsonSerializerSettings() { Formatting = Formatting.Indented}
            );
        }

        ///<summary>
        /// GET: api/quiz/Bytitle
        /// Retrieves the {num} quizzes sorted by title (A to Z)
        /// </summary>
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;

            return new JsonResult(
                sampleQuizzes.OrderBy(t => t.Title), new JsonSerializerSettings() {Formatting = Formatting.Indented}
            );
        }

        ///<summary>
        /// GET: api/quiz/mostViewed
        /// Retrieves the {num} random Quizes
        /// </summary>
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;


            return new JsonResult(
                sampleQuizzes.OrderBy(t => Guid.NewGuid()), new JsonSerializerSettings() {Formatting = Formatting.Indented}
            );
        }

    }
}
