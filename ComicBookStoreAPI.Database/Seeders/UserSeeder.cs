using ComicBookStoreAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Database.Seeders
{
    public class UserSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserSeeder(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Users.Any())
                {
                    var admin = new ApplicationUser()
                    {
                        UserName = "Admin",
                        Email = "admin@admin.com"
                    };

                    await _userManager.CreateAsync(admin, "Admin!99");

                    var roleResoult = await _userManager.AddToRoleAsync(admin, "Administrator");

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(admin);

                    await _userManager.ConfirmEmailAsync(admin, token);
                }
            }
        }
    }
}
