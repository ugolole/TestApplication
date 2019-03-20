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
        //Region RESTful conventions methods
        /// <summary>
        /// GET: api/quiz/{}id
        /// Retrieves the quiz with the given id
        /// </summary>
        /// <param name="id">The Id of the existing quiz</param>
        /// <returns>The quiz with given ID</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //create a sample quiz to match the given request
            var v = new QuizViewModel()
            {
                Id = id,
                Title = String.Format("Sample quiz with id {0}", id),
                Description = "Not a real quiz: it's jsut a sample!",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            //output the result in Json format
            return new JsonResult(v, new JsonSerializerSettings() {Formatting = Formatting.Indented });
        }

        #region 
        //region for attribute-based routing methods
        /// <summary>
        /// Get: api/quiz/latest
        /// Retrieves the {num} latest quizzes
        /// </summary>
        /// <param name="num">The number of quizzes to retrieve</param>
        /// <returns>The latest quizzes</returns>

        [HttpGet("Latest/{num:int?}")]
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
        #endregion 

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post(QuizViewModel m)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Put(QuizViewModel m)
        {
            throw new NotImplementedException();
        }
    }
}
