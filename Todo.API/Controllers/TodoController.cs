using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Controllers;
using Todo.Domain.ViewModels;
using Todo.Service.Configuration;
using Todo.Service.Interfaces.Interfaces;

namespace VUTTR.API.Controllers
{
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    public class TodoController : BaseController
    {
        private readonly ITodoService _todoService;
        private readonly List<StatusConfiguration> _statusConfigurations;

        public TodoController(ITodoService todoService, List<StatusConfiguration> statusConfigurations)
        {
            _todoService = todoService;
            _statusConfigurations = statusConfigurations;
        }

        [HttpGet("Status/")]
        public async Task<ActionResult<ResponseViewModel>> GetAlLStatus()
        {
            try
            {

                return await Task.FromResult(
                    Ok(
                        new ResponseViewModel<List<StatusConfiguration>>(true, "Registros obtidos com sucesso!", _statusConfigurations)
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e.Message)
                );
            }
        }

        [HttpGet("Admin/")]
        public async Task<ActionResult<ResponseViewModel>> GetAll(
            [FromQuery] bool somenteAtrasados = false,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        )
        {
            try
            {
                return Ok(
                    new ResponseViewModel<ICollection<TodoItemAdministradorViewModel>>(
                        true,
                        "Registros obtidos com sucesso!",
                        await _todoService.GetAll(Email, page, pageSize, somenteAtrasados)
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e.Message)
                );
            }
        }

        [HttpGet("User/")]
        public async Task<ActionResult<ResponseViewModel>> GetByUser()
        {
            try
            {
                return Ok(
                    new ResponseViewModel<ICollection<TodoItemViewModel>>(
                        true,
                        "Registros obtidos com sucesso!",
                        await _todoService.GetByUser(this.Email)
                    )
                );
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e.Message)
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseViewModel>> Insert([FromBody] TodoItemViewModel todo)
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
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e.Message)
                );
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseViewModel>> Update([FromBody] TodoItemViewModel todo)
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
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e.Message)
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
                    new ResponseViewModel(false, "Ocorreu um erro ao obter os registros!", e.Message)
                );
            }
        }
    }
}