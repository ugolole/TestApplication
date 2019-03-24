using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestApplication.ViewModels;
using System.Collections.Generic;
using TestApplication.Data;
using Mapster;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        #region Private Fields
        private ApplicationDbContext DbContext;
        #endregion

        #region Constructor
        public QuizController(ApplicationDbContext context)
        {
            //Instantiate the applicationDbContext through DI
            DbContext = context;
        }
        #endregion


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
            var quiz = DbContext.Quizzes.Where(i => i.Id == id).FirstOrDefault();

            return new JsonResult(quiz.Adapt<QuizViewModel>(), new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
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
            var latest = DbContext.Quizzes
                .OrderByDescending(q => q.CreatedDate)
                .Take(num)
                .ToArray();
            return new JsonResult(latest.Adapt<QuizViewModel[]>(), 
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        #endregion 

        ///<summary>
        /// GET: api/quiz/Bytitle
        /// Retrieves the {num} quizzes sorted by title (A to Z)
        /// </summary>
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var byTitle = DbContext.Quizzes
                .OrderBy(q => q.Title)
                .Take(num)
                .ToArray();

            return new JsonResult( byTitle.Adapt<QuizViewModel[]>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        ///<summary>
        /// GET: api/quiz/mostViewed
        /// Retrieves the {num} random Quizes
        /// </summary>
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var random = DbContext.Quizzes
                .OrderBy(q => Guid.NewGuid())
                .Take(num)
                .ToArray();

            return new JsonResult(
                random.Adapt<QuizViewModel[]>(), new JsonSerializerSettings() {
                    Formatting = Formatting.Indented
                });
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
