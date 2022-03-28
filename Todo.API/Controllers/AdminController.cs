using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.ViewModels;
using Todo.Service.Interfaces.Interfaces;

namespace VUTTR.API.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public AdminController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("Status/")]
        public async Task<ActionResult<ResponseViewModel<Dictionary<string, int>>>> GetAlLStatus()
        {
            try
            {
                var status = new Dictionary<string, int>
                {
                    {"Backlog", 0},
                    {"ToDo", 1},
                    {"Doing", 2},
                    {"Done", 3},
                };

                return await Task.FromResult(
                    Ok(
                        new ResponseViewModel<Dictionary<string, int>>(true, "Registros obtidos com sucesso!", status)
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e)
                );
            }
        }

        [HttpGet("User/{UserId}")]
        public async Task<ActionResult<ResponseViewModel<UserViewModel>>> GetByUserId([FromRoute] int UserId)
        {
            try
            {
                return Ok(
                    new ResponseViewModel<ICollection<TodoItemViewModel>>(
                        true, 
                        "Registros obtidos com sucesso!", 
                        await _todoService.GetByUserId(UserId)
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e)
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseViewModel<TodoItemViewModel>>> Insert([FromBody] TodoItemViewModel todo)
        {
            try
            {
                return Ok(
                    new ResponseViewModel<TodoItemViewModel>(
                        true,
                        "Registros inseridos com sucesso!",
                        await _todoService.Insert(todo)
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e)
                );
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseViewModel<TodoItemViewModel>>> Update([FromBody] TodoItemViewModel todo)
        {
            try
            {
                return Ok(
                    new ResponseViewModel<TodoItemViewModel>(
                        true,
                        "Registros atualizados com sucesso!",
                        await _todoService.Update(todo)
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e)
                );
            }
        }

        [HttpDelete("{TodoId}")]
        public async Task<ActionResult<ResponseViewModel>> Delete([FromRoute] int TodoId)
        {
            try
            {
                await _todoService.Delete(TodoId);

                return Ok(
                    new ResponseViewModel(
                        true,
                        "Registros deletado com sucesso!",
                        null
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e)
                );
            }
        }
    }
}