using System;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Todo.Data.Repository.Interfaces;
using Todo.Domain.ViewModels;
using Todo.Domain.Models;
using Todo.Service.Configuration;
using AutoMapper;
using Todo.Service.Interfaces.Interfaces;
using System.Linq;

namespace VUTTR.Service.Interfaces.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private TokenConfigurations _configuration;
        private List<PerfilConfiguration> _perfilConfiguration;
        private const string DATE_FORMAT = "yyy-MM-dd HH:mm:ss";
        public UserService(IUserRepository repo, ITokenService ServiceToken, TokenConfigurations configuration, List<PerfilConfiguration> perfilConfiguration,IMapper map)
        {
            _mapper = map;
            _perfilConfiguration = perfilConfiguration;
            _userRepository = repo;
            _tokenService = ServiceToken;
            _configuration = configuration;
        }
        public async Task<TokenViewModel> Login(UserViewModel user)
        {
            var model = _mapper.Map<User>(user);
            var userModel = await _userRepository.Login(model);

            if (userModel == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim("Email", user.Email)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _userRepository.RefreshUserInfo(model);

            var createDate = DateTime.Now;
            var ExpirationDate = createDate.AddMinutes(_configuration.Minutes);

            userModel.Password = null;
            return new TokenViewModel(
                true,
                createDate.ToString(DATE_FORMAT),
                ExpirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken,
                _mapper.Map<UserViewModel>(userModel)
                );
        }

        public async Task<UserViewModel> Register(UserViewModel user)
        {
            var model = _mapper.Map<User>(user);

            ValidarPerfil((int)model.Perfil);

            var modelUserName = await this.GetByEmail(user.Email);
            if (modelUserName != null)
                throw new Exception("Usuário já existente!");

            model.Password = _userRepository.ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _mapper.Map<UserViewModel>(await _userRepository.Register(model));
        }

        private void ValidarPerfil(int perfil)
        {
            var res = _perfilConfiguration.Where(x => x.Id == perfil).ToList();
            if (res.Count == 0)
                throw new Exception("Perfil não existe!");
        }

        public async Task<UserViewModel> Update(UserViewModel user)
        {
            if (user.Password == null)
            {
                var modelComPassword = await this.GetById(user.UserId, true);
                user.Password = modelComPassword.Password;
            }
            else
            {
                user.Password = _userRepository.ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            }
            var model = _mapper.Map<User>(user);
            ValidarPerfil((int)model.Perfil);

            return _mapper.Map<UserViewModel>(await _userRepository.Update(model));
        }

        public async Task<UserViewModel> GetById(int UserId, bool notIncludePassword)
        {
            var model = await _userRepository.GetById(UserId);
            if (model == null && notIncludePassword)
                model.Password = null;

            return _mapper.Map<UserViewModel>(model);
        }

        public async Task<UserViewModel> GetByEmail(string email)
        {
            var model = await _userRepository.GetByEmail(email);
            if (model == null)
            {
                return null;
            }
            return _mapper.Map<UserViewModel>(model);
        }
    }
}