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

            //handle request asking for non-existing quizzes 
            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Quiz ID {0} has not been found", id)
                });
            }

            return new JsonResult(quiz.Adapt<QuizViewModel>(), new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// Adds new quiz to the database
        /// </summary>
        /// <param name="model">Teh QuizViewModel containing the data to insert</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]QuizViewModel model)
        {
            //return a generic HTTP Status 500 (Server Error)
            //if the client payload is invalid
            if (model == null) return new StatusCodeResult(500);

            //handle the insert (without object-mapping).
            var quiz = new Quiz();

            //properties taken from the request
            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            //properties set from the server side
            quiz.CreatedDate = DateTime.Now;
            quiz.LastModifiedDate = quiz.CreatedDate;

            //set a temporary author using the Admin user's userId
            //as user login is not supported yet:
            quiz.UserId = DbContext.Users.Where(u => u.UserName == "Admin").FirstOrDefault().Id;

            // add the new quiz
            DbContext.Quizzes.Add(quiz);
            //persist the changes into the Database.
            DbContext.SaveChanges();

            //return the newly created quiz to the client
            return new JsonResult(quiz.Adapt<QuizViewModel>(), new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// Edit the quiz with given {id}
        /// </summary>
        /// <param name="model">The QuizViewModel containing the data to update</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]QuizViewModel model)
        {
            //return generic HTTP Status 500 (Server Error)
            //if the client payload is invalid
            if (model == null) return new StatusCodeResult(500);

            //retrieve the quiz to edit
            var quiz = DbContext.Quizzes.Where(q => q.Id == model.Id).FirstOrDefault();

            //handle requests asking for non-existing quizzes 
            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Quiz ID {0} has not been found", model.Id)
                });
            }

            //handle the update (without object - mapping)
            // by manually assigning properties
            //we want to accept form the request
            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            //properties set from server-side
            quiz.LastModifiedDate = quiz.CreatedDate;

            //persist the changes to the database
            DbContext.SaveChanges();

            //return the update Quiz to the client
            return new JsonResult(quiz.Adapt<QuizViewModel>(), new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// Delete the quiz with given {ID} from the database
        /// </summary>
        /// <param name="id">The Id of the existing test</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //retrieve the quiz from the database
            var quiz = DbContext.Quizzes.Where(i => i.Id == id).FirstOrDefault();

            //handle request asking for non-existing quizzes
            if(quiz == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Quiz ID {0} has not been found", id)
                });
            }

            //remove teh quiz from the DbContext
            DbContext.Quizzes.Remove(quiz);

            //persist the changes into the Databases
            DbContext.SaveChanges();

            //return an HTTP Status 200 (ok)
            return new OkResult();
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

            return new JsonResult(byTitle.Adapt<QuizViewModel[]>(),
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
                random.Adapt<QuizViewModel[]>(), new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

    }
}
