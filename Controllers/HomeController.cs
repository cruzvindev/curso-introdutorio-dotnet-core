using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Data;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Controllers;

[Route("api/todo")]
[ApiController] //Declara que é um controller de API(devolve JSON)
public class HomeController : ControllerBase //Transforma a classe um controller
{
    [HttpGet("listar")]
    public List<TodoModel> Get([FromServices] AppDbContext context)
    {
        return context.Todos.ToList();
    }

    [HttpPost("cadastrar")]
    public TodoModel Post([FromBody]TodoModel model ,[FromServices] AppDbContext context)
    {
        context.Todos.Add(model);
        context.SaveChanges();

        return model;
    }

    [HttpGet("/{todoId:int}")]
    public TodoModel GetById([FromRoute] int todoId, [FromServices] AppDbContext context)
    {
        return context.Todos.FirstOrDefault(todo => todo.Id == todoId);
    }

    [HttpPut("/{todoId:int}")]
    public IActionResult Put([FromRoute] int todoId, [FromBody] TodoModel model, [FromServices] AppDbContext context)
    {
        var todoModel = context.Todos.FirstOrDefault(todo => todo.Id == todoId);
        if (todoModel == null)
            return NotFound();
        
        todoModel.Title = model.Title;
        todoModel.Done = model.Done;

        context.Todos.Update(todoModel);
        context.SaveChanges();

        return Ok(todoModel);
    }


    [HttpDelete("/{todoId:int}")]
    public IActionResult Delete([FromRoute] int todoId, [FromServices] AppDbContext context)
    {
        var todoModel = context.Todos.FirstOrDefault(todo => todo.Id == todoId);
        if (todoModel == null)
            return NotFound();

        context.Todos.Remove(todoModel);
        context.SaveChanges();

        return NoContent();
    }
}
