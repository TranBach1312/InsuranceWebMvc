using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace Project3.Service
{
    public class MediaService
    {
        private readonly IUrlHelper _urlHelper;
        public MediaService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new BadRequestObjectResult("No file uploaded");
            }
            string extension = Path.GetExtension(file.FileName);
            string newName = Guid.NewGuid().ToString() + extension;
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images",
                newName);
            var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            string imageUrl = _urlHelper.Content("~/images/" + file.FileName);
            return new OkObjectResult(imageUrl);
        }
    }
}
