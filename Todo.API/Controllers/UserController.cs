using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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


        /// <summary>
        /// Get all perfil
        /// </summary>
        /// <returns>List with all perfil</returns>
        [HttpGet("Perfis/")]
        [Authorize("Bearer")]
        [ProducesResponseType(typeof(ResponseViewModel<List<PerfilConfiguration>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseViewModel>> GetAllPerfil()
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

        /// <summary>
        /// Login in API
        /// </summary>
        /// <returns>Token from user</returns>
        [HttpPost("login")]
        [AllowAnonymousAttribute]
        [ProducesResponseType(typeof(ResponseViewModel<TokenViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Register in API
        /// </summary>
        /// <returns>User registed</returns>
        [HttpPost("register")]
        [AllowAnonymousAttribute]
        [ProducesResponseType(typeof(ResponseViewModel<UserViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Update data to User
        /// </summary>
        /// <returns>User updated</returns>
        [HttpPut]
        [Authorize("Bearer")]
        [ProducesResponseType(typeof(ResponseViewModel<UserViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel), StatusCodes.Status400BadRequest)]
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