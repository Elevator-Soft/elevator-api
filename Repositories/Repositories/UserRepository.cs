using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Database;
using Repositories.Database.Models;

namespace Repositories.Repositories
{
    public class UserRepository: AbstractRepository<User>
    {
        public UserRepository(DatabaseContext dbContext) : base(dbContext, dbContext.Users)
        {
        }
    }
}
