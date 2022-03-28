using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Data.Context;
using Todo.Data.Repository.Interfaces;
using Todo.Domain.Models;

namespace Todo.Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoContext _ctx;

        public UserRepository(TodoContext context)
        {
            _ctx = context;
        }
        public async Task<User> Login(User user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return await _ctx.Users
                        .FirstOrDefaultAsync(x => (x.Password.Equals(pass)
                                                && x.Email.Equals(user.Email)));
        }

        public async Task<User> Register(User user)
        {
            await _ctx.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public async Task<User> RefreshUserInfo(User user)
        {
            try
            {
                var result = await GetById(user.UserId);
                if (result != null)
                {
                    _ctx.Entry(result).CurrentValues.SetValues(user);
                    await _ctx.SaveChangesAsync();
                    return user;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _ctx.Users
                        .FirstOrDefaultAsync(u => u.Email.Contains(email));
        }

        public async Task<User> Update(User user)
        {
            var result = await _ctx.Users
                        .FirstOrDefaultAsync(t => t.UserId.Equals(user.UserId));

            _ctx.Entry(result).CurrentValues.SetValues(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetById(int UserId)
        {
            return await _ctx.Users
                                    .FirstOrDefaultAsync(u => u.UserId.Equals(UserId));
        }
    }
}