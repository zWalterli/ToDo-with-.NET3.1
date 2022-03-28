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
using Xunit;
using Moq;
using System;

namespace Todo.Tests.Unitary.Tests
{
    public class TodoServiceTest
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ITodoRepository> _todoRepository;
        private readonly Mock<ITokenService> _tokenService;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<TokenConfigurations> _configuration;
        private readonly List<StatusConfiguration> _statusConfiguration;
        private readonly TodoService todoService;

        public TodoServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _todoRepository = new Mock<ITodoRepository>();
            _tokenService = new Mock<ITokenService>();
            _userService = new Mock<IUserService>();
            _configuration = new Mock<TokenConfigurations>();
            _statusConfiguration = new List<StatusConfiguration>
            {
                new StatusConfiguration(0, "Backlog"),
                new StatusConfiguration(1, "ToDo"),
                new StatusConfiguration(2, "Doing"),
                new StatusConfiguration(3, "Done")
            };

            todoService = new TodoService(
                _todoRepository.Object, 
                _tokenService.Object, 
                _userService.Object, 
                _configuration.Object, 
                _statusConfiguration, 
                _mapper.Object
            );
        }

        [Fact]
        public async Task GetAll_PassandoValoresValidos_RetornandoListaDeToDos()
        {
            // Arrange
            _userService.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(UserViewModelMock.UserViewModelMock_Admin()));

            _todoRepository.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(TodoItemMock.TodoItemListMock()));

            // Act
            var res = await todoService.GetAll(
                email: "teste@email.com",
                page: 1,
                pageSize: 10,
                onlyLate: false
            );

            // Assert
            Assert.True(res.Count > 0);
            Assert.NotNull(res);
        }

        [Fact]
        public async Task GetByUser_PassandoValoresValidos_RetornandoListaDeToDo()
        {
            // Arrange
            _mapper.Setup(x => x.Map<List<TodoItem>>(It.IsAny<List<TodoItemViewModel>>()))
                .Returns(TodoItemMock.TodoItemListMock());

            _mapper.Setup(x => x.Map<List<TodoItemViewModel>>(It.IsAny<List<TodoItem>>()))
                .Returns(TodoItemViewModelMock.TodoItemViewModelListMock());

            _userService.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(UserViewModelMock.UserViewModelMock_User()));

            _todoRepository.Setup(x => x.GetByUserId(It.IsAny<int>()))
                .Returns(Task.FromResult(TodoItemMock.TodoItemListMock()));

            // Act
            var res = await todoService.GetByUser(email: "email@teste.com.br");

            // Assert
            Assert.True(res.Count > 0);
            Assert.NotNull(res);
        }

        [Fact]
        public async Task GetByUser_PassandoValoresInvalidos_RetornandoException()
        {
            // Arrange
            // ...

            // Act
            var exception = await Record.ExceptionAsync(async () => await todoService.GetByUser(email: string.Empty));

            // Assert
            Assert.Equal("Email inválido!", exception.Message);
            Assert.IsType<Exception>(exception);
        }

        [Fact]
        public async Task GetById_PassandoValoresValidos_RetornandoUmRegistro()
        {
            // Arrange
            _mapper.Setup(x => x.Map<TodoItem>(It.IsAny<TodoItemViewModel>()))
                .Returns(TodoItemMock.TodoItemOneMock());

            _mapper.Setup(x => x.Map<TodoItemViewModel>(It.IsAny<TodoItem>()))
                .Returns(TodoItemViewModelMock.TodoItemViewModelOneMock());

            _todoRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(TodoItemMock.TodoItemOneMock()));

            // Act
            var res = await todoService.GetById(todoId: 1);

            // Assert
            Assert.NotNull(res);
            Assert.True(typeof(TodoItemViewModel) == res.GetType());
        }

        [Fact]
        public async Task GetById_PassandoValoresInvalidos_RetornandoException()
        {
            // Arrange
            // ...

            // Act
            var exception = await Record.ExceptionAsync(async () => await todoService.GetById(todoId: 0));

            // Assert
            Assert.Equal("Identificador inválido!", exception.Message);
            Assert.IsType<Exception>(exception);
        }

        [Fact]
        public async Task Insert_PassandoValoresValidos_RetornandoUmRegistro()
        {
            // Arrange
            _mapper.Setup(x => x.Map<TodoItem>(It.IsAny<TodoItemViewModel>()))
                .Returns(TodoItemMock.TodoItemOneMock());

            _mapper.Setup(x => x.Map<TodoItemViewModel>(It.IsAny<TodoItem>()))
                .Returns(TodoItemViewModelMock.TodoItemViewModelOneMock());

            _userService.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(UserViewModelMock.UserViewModelMock_User()));

            _todoRepository.Setup(x => x.Insert(It.IsAny<TodoItem>()))
                .Returns(Task.FromResult(TodoItemMock.TodoItemOneMock()));

            _todoRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(TodoItemMock.TodoItemOneMock()));

            // Act
            var res = await todoService.Insert(todo: TodoItemViewModelMock.TodoItemViewModelOneMock());

            // Assert
            Assert.NotNull(res);
            Assert.True(typeof(TodoItemViewModel) == res.GetType());
        }

        [Fact]
        public async Task Update_PassandoValoresValidos_RetornandoUmRegistro()
        {
            // Arrange
            _mapper.Setup(x => x.Map<TodoItem>(It.IsAny<TodoItemViewModel>()))
                .Returns(TodoItemMock.TodoItemOneMock());

            _mapper.Setup(x => x.Map<TodoItemViewModel>(It.IsAny<TodoItem>()))
                .Returns(TodoItemViewModelMock.TodoItemViewModelOneMock());

            _userService.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(Task.FromResult(UserViewModelMock.UserViewModelMock_User()));

            _todoRepository.Setup(x => x.Update(It.IsAny<TodoItem>()))
                .Returns(Task.FromResult(TodoItemMock.TodoItemOneMock()));

            _todoRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(Task.FromResult(TodoItemMock.TodoItemOneMock()));

            // Act
            var res = await todoService.Update(todo: TodoItemViewModelMock.TodoItemViewModelOneMock());

            // Assert
            Assert.NotNull(res);
            Assert.True(typeof(TodoItemViewModel) == res.GetType());
        }

        [Fact]
        public async Task Delete_PassandoValoresValidos_SemRetorno()
        {
            // Arrange
            _mapper.Setup(x => x.Map<TodoItem>(It.IsAny<TodoItemViewModel>()))
                .Returns(TodoItemMock.TodoItemOneMock());

            _mapper.Setup(x => x.Map<TodoItemViewModel>(It.IsAny<TodoItem>()))
                .Returns(TodoItemViewModelMock.TodoItemViewModelOneMock());

            _todoRepository.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(Task.FromResult(TodoItemMock.TodoItemOneMock()));

            // Act
            await todoService.Delete(todoId: 1);

            // Assert
            Assert.True(true);
        }
    }
}
