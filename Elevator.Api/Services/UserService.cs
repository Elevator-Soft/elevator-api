using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elevator.Api.Exceptions;
using Elevator.Api.Services.Interfaces;
using Elevator.Api.Utils.Mapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Models;
using Repositories.Repositories;

namespace Elevator.Api.Services
{
    public class UserService: IUserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var dbUser = await userRepository.FindByIdAsync(id);
            if (dbUser == null)
                throw new EntityNotFoundException(nameof(User), id);

            return ModelsMapper.ConvertUserDatabaseModelToService(dbUser);
        }

        public async Task RegisterUser(User user)
        {
            var dbUser = ModelsMapper.ConvertUserServiceModelToDatabase(user);
            await userRepository.AddAsync(dbUser);
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var dbUser = await userRepository.FindByIdAsync(id);
            return dbUser != null;
        }
    }
}
