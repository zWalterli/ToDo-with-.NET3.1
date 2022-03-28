using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.ViewModels;
using Todo.Service.Configuration;
using Todo.Service.Interfaces.Interfaces;

namespace VUTTR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly List<PerfilConfiguration> _perfilConfigurations;
        private readonly IUserService _userService;

        public UserController(IUserService service, List<PerfilConfiguration> perfilConfigurations)
        {
            _userService = service;
            _perfilConfigurations = perfilConfigurations;
        }

        [HttpGet("Perfis/")]
        [Authorize("Bearer")]
        public async Task<ActionResult<ResponseViewModel>> GetAllPerfis()
        {
            try
            {
                return await Task.FromResult(
                    Ok(
                        new ResponseViewModel<List<PerfilConfiguration>>(true, "Registros obtidos com sucesso!", _perfilConfigurations)
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

        [HttpGet("{UserId}")]
        [Authorize("Bearer")]
        public async Task<ActionResult<ResponseViewModel>> GetById([FromRoute] int UserId)
        {
            try
            {
                return Ok(
                    new ResponseViewModel<UserViewModel>(
                        true,
                        "Registros obtidos com sucesso!",
                        await _userService.GetById(UserId, false)
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

        [HttpPost("login")]
        [AllowAnonymousAttribute]
        public async Task<ActionResult<ResponseViewModel>> Login([FromBody] UserViewModel user)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Modelo passado não é válido!");

                var token = await _userService.Login(user);

                if (token == null)
                    return Unauthorized(
                        new ResponseViewModel<TokenViewModel>(
                            true,
                            "Acesso negado!",
                            null
                        )
                    );
                else
                    return Ok(
                        new ResponseViewModel<TokenViewModel>(
                            true,
                            "Registros obtidos com sucesso!",
                            token
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

        [HttpPost("register")]
        [AllowAnonymousAttribute]
        public async Task<ActionResult<ResponseViewModel>> Register([FromBody] UserViewModel user)
        {
            try
            {
                return Ok(
                    new ResponseViewModel<UserViewModel>(
                        true,
                        "Registros obtidos com sucesso!",
                        await _userService.Register(user)
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
        [Authorize("Bearer")]
        public async Task<ActionResult<ResponseViewModel>> Update([FromBody] UserViewModel user)
        {
            try
            {
                return Ok(
                    new ResponseViewModel<UserViewModel>(
                        true,
                        "Registros obtidos com sucesso!",
                        await _userService.Update(user)
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