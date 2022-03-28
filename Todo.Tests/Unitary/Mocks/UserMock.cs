using Todo.Domain.Models;

namespace Todo.Tests.Unitary.Mocks
{
    public static class UserMock
    {
        public static User UserMock_User()
            => new User
            {
                Email = "TESTE@TESTE.com.br",
                FullName = "TESTE",
                Password = "8D-96-9E-EF-6E-CA-D3-C2-9A-3A-62-92-80-E6-86-CF-0C-3F-5D-5A-86-AF-F3-CA-12-02-0C-92-3A-DC-6C-92",
                Perfil = Domain.Enum.PerfilEnum.USER,
                UserId = 0
            };
        public static User UserMock_Admin()
            => new User
            {
                Email = "TESTE@TESTE.com.br",
                FullName = "TESTE",
                Password = "8D-96-9E-EF-6E-CA-D3-C2-9A-3A-62-92-80-E6-86-CF-0C-3F-5D-5A-86-AF-F3-CA-12-02-0C-92-3A-DC-6C-92",
                Perfil = Domain.Enum.PerfilEnum.USER,
                UserId = 1
            };
    }
}
