using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.DataBase;

namespace Server.Repositories
{
    public class BaseRepository
    {
        protected readonly ApplicationDbContext dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}