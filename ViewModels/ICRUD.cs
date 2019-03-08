using System;
using Microsoft.AspNetCore.Mvc;
namespace TestApplication.ViewModels
{
    public interface ICRUD
    {
        [HttpGet("{id}")]
        IActionResult Get(int id);
        [HttpGet]
        IActionResult Put(object m);
        [HttpPost]
        IActionResult Post(object m);
        [HttpDelete]
        IActionResult Delete(int Id);
    }
}
