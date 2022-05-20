using Microsoft.AspNetCore.Identity;


namespace ComicBookStoreAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public string AvatarPictureName { get; set; }
        public virtual List<Rating> Ratings { get; set; }
    }
}
