using GGWalks.API.Models.Domain;
using GGWalks.API.Models.DTO;
using GGWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GGWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly ILogger<ImagesController> logger;

        public ImagesController(IImageRepository imageRepository, ILogger<ImagesController> logger)
        {
            this.imageRepository = imageRepository;
            this.logger = logger;
        }

        //Uploading images
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImages([FromForm] ImageUploadRequestDto request )
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                //convert DTO to domain
                var imageDomain = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    Filename = request.Filename,
                    FileDescription = request.FileDescription

                };
                //use repo to upload image
                await imageRepository.UploadImage(imageDomain);
                return Ok(imageDomain);

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpeg", ".jpg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extensions");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10 MB, please upload smaller size file.");
            }

        }
    }
}
