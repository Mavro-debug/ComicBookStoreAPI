using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ComicBookStoreAPI.Controllers
{
    [Route("file")]
    public class FileController : ControllerBase
    {
        [Route("getBaseAvatar")]
        public IActionResult GetBaseAvatar()
        {
            var rootPath = Directory.GetCurrentDirectory();

            var filePath = $"{rootPath}/Files/Avatars/unknown.jpg";

            var fileExists = System.IO.File.Exists(filePath);

            if (!fileExists)
            {
                return NotFound();
            }

            var extentionProvider = new FileExtensionContentTypeProvider();
            extentionProvider.TryGetContentType("unknown.jpg", out string contentType);

            var fileContent = System.IO.File.ReadAllBytes(filePath);

            return File(fileContent, contentType, "unknownAvatar");
        }



    }
}
