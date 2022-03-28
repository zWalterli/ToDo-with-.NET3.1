using AutoMapper;
using Todo.Domain.ViewModels;
using Todo.Domain.Models;

namespace Todo.API.Configurations
{
    public class MapConfiguration : Profile
    {
        
        public MapConfiguration()
        {
            #region User
            CreateMap<UserViewModel, User>().ReverseMap();
            #endregion

            #region Todo
            CreateMap<TodoItemViewModel, TodoItem>().ReverseMap();
            #endregion

                #region Token
            CreateMap<TokenViewModel, Token>().ReverseMap();
            #endregion
        }
    }
}