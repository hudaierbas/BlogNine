﻿using BlogNine.Data;
using BlogNine.Data.Models;
using BlogNine.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNine.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public ApplicationUser Get(string id)
        {
            return applicationDbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public async Task<ApplicationUser> Update(ApplicationUser applicationUser)
        {
            applicationDbContext.Update(applicationUser);
            await applicationDbContext.SaveChangesAsync();
            return applicationUser;
        }
    }
}
