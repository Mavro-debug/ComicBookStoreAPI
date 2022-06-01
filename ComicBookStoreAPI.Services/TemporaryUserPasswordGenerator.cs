using System.Text;

namespace ComicBookStoreAPI.Services
{
    public static class TemporaryUserPasswordGenerator
    {
        public static string GeneratePassword(string name, string lastName)
        {
            StringBuilder passwordBuilder = new StringBuilder();

            var namePart = name.Trim();

            passwordBuilder.Append(char.ToUpper(namePart[0]));
            passwordBuilder.Append(namePart.Substring(1, 2));
            passwordBuilder.Append("!");

            var lastNamePart = lastName.Trim();
            passwordBuilder.Append(char.ToUpper(lastNamePart[0]));
            passwordBuilder.Append(lastNamePart.Substring(1, 2));

            Random rnd = new Random();

            passwordBuilder.Append(rnd.Next(10, 99));

            return passwordBuilder.ToString();
        }
    }
}
