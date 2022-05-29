using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Models
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string? AvatarPictureName { get; set; }
    }
}
