using VUTTR.Service.Interfaces.Implementations;
using Todo.Service.Interfaces.Interfaces;
using Todo.Data.Repository.Interfaces;
using Todo.Service.Configuration;
using System.Collections.Generic;
using Todo.Tests.Unitary.Mocks;
using Todo.Domain.ViewModels;
using System.Threading.Tasks;
using Todo.Domain.Models;
using AutoMapper;
using System;
using Xunit;
using Moq;

namespace Todo.Tests.Unitary.Tests
{
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ITokenService> _serviceToken;
        private readonly Mock<TokenConfigurations> _configuration; 
        private readonly List<PerfilConfiguration> _perfilConfiguration;
        private readonly Mock<IMapper> _map;


        public UserServiceTest() {
            _userRepository = new Mock<IUserRepository>();
            _serviceToken = new Mock<ITokenService>();
            _configuration = new Mock<TokenConfigurations>();
            _perfilConfiguration = new List<PerfilConfiguration>
            {
                new PerfilConfiguration(0, "User"),
                new PerfilConfiguration(1, "ADM"),
            };
            _map = new Mock<IMapper>();

            _userService = new UserService(
                _userRepository.Object,
                _serviceToken.Object,
                _configuration.Object,
                _perfilConfiguration,
                _map.Object
            );
        }

        
        [Fact]
        public async Task Login_PassandoValoresValidos_RetornandoTokenValido()
        {
            // Arrange
            _map.Setup(x => x.Map<UserViewModel>(It.IsAny<User>()))
                .Returns(UserViewModelMock.UserViewModelMock_Admin());
            _map.Setup(x => x.Map<User>(It.IsAny<UserViewModel>()))
                .Returns(UserMock.UserMock_Admin());

            _userRepository.Setup(x => x.Login(It.IsAny<User>()))
                .Returns(Task.FromResult(UserMock.UserMock_Admin()));

            // Act
            var res = await _userService.Login(UserViewModelMock.UserViewModelMock_User());

            // Arrange
            Assert.NotNull(res);
        }
        
        [Fact]
        public async Task Login_PassandoValoresInvalidos_RetornandoNull()
        {
            // Arrange
            _map.Setup(x => x.Map<UserViewModel>(It.IsAny<User>()))
                .Returns(UserViewModelMock.UserViewModelMock_Admin());
            _map.Setup(x => x.Map<User>(It.IsAny<UserViewModel>()))
                .Returns(UserMock.UserMock_Admin());

            // Act
            var res = await _userService.Login(UserViewModelMock.UserViewModelMock_User());

            // Arrange
            Assert.Null(res);
        }
        
        [Fact]
        public async Task Register_PassandoValoresValidos_RetornandoUserViewModel()
        {
            // Arrange
            _map.Setup(x => x.Map<UserViewModel>(It.IsAny<User>()))
                .Returns(UserViewModelMock.UserViewModelMock_Admin());
            _map.Setup(x => x.Map<User>(It.IsAny<UserViewModel>()))
                .Returns(UserMock.UserMock_Admin());
                                
            // Act
            var res = await _userService.Register(UserViewModelMock.UserViewModelMock_User());

            // Arrange
            Assert.NotNull(res);
        }
        
        [Fact]
        public async Task Register_PassandoValoresInvalidos_UsuarioJaExistente_RetornandoException()
        {
            // Arrange
            _map.Setup(x => x.Map<UserViewModel>(It.IsAny<User>()))
                .Returns(UserViewModelMock.UserViewModelMock_Admin());
            _map.Setup(x => x.Map<User>(It.IsAny<UserViewModel>()))
                .Returns(UserMock.UserMock_Admin());
                
            _userRepository.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(UserMock.UserMock_Admin()));
                
            // Act
            var exception = await Record.ExceptionAsync(async () => await _userService.Register(UserViewModelMock.UserViewModelMock_User()));

            // Arrange
            Assert.Equal("Usuário já existente!", exception.Message);
            Assert.IsType<Exception>(exception);
        }

    }
}
