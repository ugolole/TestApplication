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
    public class ResultController : Controller
    {
        [HttpGet("all/{quizId}")]
       public IActionResult All(int quizId ) 
       { 
            //create the sample result variable
            var sampleResult = new List<ResultViewModel>();

            sampleResult.Add(new ResultViewModel()
            {
                Id = 1,
                QuizId = quizId,
                Text = "What do you value most in your life?",
                CreateDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for(int i = 2; i <= 5; i++)
            {
                sampleResult.Add(new ResultViewModel()
                {
                    Id = i,
                    QuizId = quizId,
                    Text = String.Format("Sample result {0}", i),
                    CreateDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(sampleResult, new JsonSerializerSettings() {Formatting = Formatting.Indented }); 
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
        public IActionResult Post(ResultViewModel m)
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        public IActionResult Put(ResultViewModel m)
        {
            throw new NotImplementedException();
        }
    }
}
