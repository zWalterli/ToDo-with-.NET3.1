using Todo.Service.Interfaces.Interfaces;
using Todo.Data.Repository.Interfaces;
using Todo.Service.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.ViewModels;
using Todo.Domain.Models;
using System.Linq;
using AutoMapper;
using System;

namespace VUTTR.Service.Interfaces.Implementations
{
    public class TodoService : ITodoService
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _todoRepository;
        private readonly IUserService _userService;
        private readonly List<StatusConfiguration> _statusConfiguration;
        public TodoService(ITodoRepository repo, ITokenService ServiceToken, IUserService userService, TokenConfigurations configuration, List<StatusConfiguration> statusConfiguration, IMapper map)
        {
            _mapper = map;
            _statusConfiguration = statusConfiguration;
            _todoRepository = repo;
            _userService = userService;
        }

        private void ValidarStatus(int status)
        {
            var res = _statusConfiguration.Where(x => x.Id == status).ToList();
            if (res.Count == 0)
                throw new Exception("Status não existe!");
        }

        private async Task ValidaUsuarioAdministrador(string email)
        {
            var user = await _userService.GetByEmail(email) ?? throw new Exception("Nenhum usuário encontrado!");
            if (user.Perfil != Todo.Domain.Enum.PerfilEnum.ADMIN)
                throw new Exception("Você não tem o perfil de ADMINISTRADOR!");
        }

        public async Task<List<TodoItemViewModel>> GetByUser(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("Email inválido!");
            
            var user = await _userService.GetByEmail(email);
            var todosModel = await _todoRepository.GetByUserId(user.UserId);
            return _mapper.Map<List<TodoItemViewModel>>(todosModel);
        }

        public async Task<TodoItemViewModel> GetById(int todoId)
        {
            if (todoId < 1)
                throw new Exception("Identificador inválido!");

            var todoModel = await _todoRepository.GetById(todoId);
            return _mapper.Map<TodoItemViewModel>(todoModel);
        }

        public async Task<TodoItemViewModel> Insert(TodoItemViewModel todo)
        {
            var todoModel = _mapper.Map<TodoItem>(todo);

            if (!todoModel.isValid)
                throw new Exception("Informações não são válidas");

            ValidarStatus((int)todoModel.Status);

            todoModel.GenerateDateInsertion();

            var todosModelAfterInsert = await _todoRepository.Insert(todoModel);
            return _mapper.Map<TodoItemViewModel>(todosModelAfterInsert);
        }

        public async Task<TodoItemViewModel> Update(TodoItemViewModel todo)
        {
            if (!todo.TodoId.HasValue)
                throw new Exception("Não foi possível capturar o identificador.");

            var todoInBD = await GetById(todo.TodoId.Value);
            if (todoInBD.Status == Todo.Domain.Enum.StatusTodoEnum.DONE)
                throw new Exception("Não foi possível atualizar um item TODO que já foi finalizado anteriormente.");

            var todoModel = _mapper.Map<TodoItem>(todo);
            if (!todoModel.isValid)
                throw new Exception("Informações não são válidas");

            ValidarStatus((int)todoModel.Status);

            if (todoModel.Status == Todo.Domain.Enum.StatusTodoEnum.DONE)
                todoModel.GenerateDateConclusion();

            todoModel.DateInsertion = todoInBD.DateInsertion;
            todoModel.DateLastUpdate = DateTime.Now;

            var todosModelAfterUpdate = await _todoRepository.Update(todoModel);
            return _mapper.Map<TodoItemViewModel>(todosModelAfterUpdate);
        }

        public async Task Delete(int todoId)
        {
            if (todoId < 1)
                throw new Exception("Identificador inválido!");

            await _todoRepository.Delete(todoId);
        }

        public async Task<List<TodoItemAdministradorViewModel>> GetAll(string email, int page, int pageSize, bool onlyLate)
        {
            await ValidaUsuarioAdministrador(email);

            var todos = await _todoRepository.GetAll(page, pageSize);

            if(onlyLate)
                todos = todos.Where(x => x.Deadline < DateTime.Now).ToList();

            var result = new List<TodoItemAdministradorViewModel>();
            foreach (var todo in todos)
            {
                result.Add(
                    new TodoItemAdministradorViewModel
                    {
                        Email = todo.User.Email,
                        Deadline = todo.Deadline.GetValueOrDefault(),
                        Description = todo.Description,
                    }
                );
            }

            return result;
        }
    }
}