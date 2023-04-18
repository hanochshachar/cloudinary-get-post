using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using cloudinaryImg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cloudinaryImg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserImgDbContext _userDbContext;
        private readonly Cloudinary cloudinary;

        public ImagesController(UserImgDbContext userDbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            Account account = new Account(
                _configuration["Cloudinary:CloudName"],
            _configuration["Cloudinary:ApiKey"],
            _configuration["Cloudinary:ApiSecret"]);
            cloudinary = new Cloudinary(account);
            _userDbContext = userDbContext;
        }
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(IFormFile image, [FromForm] UserImage user)
        {
            // Upload
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            user.ImageUrl = uploadResult.SecureUri.AbsoluteUri; // Set the Cloudinary URL as the user's image URL
            _userDbContext.UserImage.Add(user);
            await _userDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddUser), user);
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IEnumerable<UserImage>> GetUsers()
        {
            return await _userDbContext.UserImage.ToListAsync();
        }

    }
}
