using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Get all status to To Do 
        /// </summary>
        /// <returns>List with all status</returns>
        [ProducesResponseType(typeof(ResponseViewModel<List<StatusConfiguration>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        [HttpGet("Status/")]
        public async Task<ActionResult<ResponseViewModel>> GetAllStatus()
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

        /// <summary>
        /// Get all To do
        /// </summary>
        /// <returns>List with all to do id</returns>
        [ProducesResponseType(typeof(ResponseViewModel<List<TodoItemAdministradorViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
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
                    new ResponseViewModel<List<TodoItemAdministradorViewModel>>(
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

        /// <summary>
        /// Get all To do by UserId
        /// </summary>
        /// <returns>List with To do by UserId</returns>
        [ProducesResponseType(typeof(ResponseViewModel<List<TodoItemViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        [HttpGet("User/")]
        public async Task<ActionResult<ResponseViewModel>> GetByUser()
        {
            try
            {
                return Ok(
                    new ResponseViewModel<List<TodoItemViewModel>>(
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

        /// <summary>
        /// Insert one To do
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>To do inserted</returns>
        [ProducesResponseType(typeof(ResponseViewModel<TodoItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<ResponseViewModel>> Insert([FromBody] TodoItemViewModel todo)
        {
            try
            {
                return Ok(
                    new ResponseViewModel<TodoItemViewModel>(
                        true,
                        "Registros inseridos com sucesso!",
                        await _todoService.Insert(todo, Email)
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

        /// <summary>
        /// Update one To do
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>To do updated</returns>
        [ProducesResponseType(typeof(ResponseViewModel<TodoItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<ResponseViewModel>> Update([FromBody] TodoItemViewModel todo)
        {
            try
            {
                return Ok(
                    new ResponseViewModel<TodoItemViewModel>(
                        true,
                        "Registros atualizados com sucesso!",
                        await _todoService.Update(todo, Email)
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

        /// <summary>
        /// Delete one To do by Id
        /// </summary>
        /// <param name="TodoId">ToDo Id</param>
        /// <returns>No content</returns>

        [HttpDelete("{TodoId}")]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
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