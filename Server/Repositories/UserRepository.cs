using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.DataBase;
using Server.Models;

namespace Server.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddUserToDataBase(User user)
        {
            dbContext.Users.Add(user);
            await Save();
        }

        public async Task<bool> CheckEmail(string email)
        {
            return await dbContext.Users
                .AnyAsync(u => u.Email == email);
        }
        
        public async Task<bool> CheckUserName(string userName)
        {
            return await dbContext.Users
                .AnyAsync(u => u.UserName == userName);
        }

        public async Task<User> GetUserFromDataBase(string email, string password)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            
            if (user == null)
            {
                throw new Exception("Пользователь не найден в базе данных");
            }
            return user;
        }
    }
}