using FindAFriend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindAFriend.Models;

namespace FindAFriend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProfilesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProfilesApiController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("loadprofiles")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            List<Profile> profiles = _context.Profile.ToList();
            return profiles;
        }


        [HttpPost]
        [Route("postprofile")]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {

            Profile newProfile = new Profile
            {
                Name = profile.Name,
                Birthday = DateTime.Now,
                Description = profile.Description,
                City = profile.City,
                UserID = profile.UserID
            };

            _context.Profile.Add(newProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = newProfile.ProfileID }, newProfile);
        }

        [HttpPost]
        [Route("postimages")]
        public async Task<ActionResult<Profile>> PostImages(ImageModel imageModel)
        {

            ImageModel newImageModel = new ImageModel
            {
                UserEmail = imageModel.UserEmail + imageModel.ImageExtension,
                ImageExtension = imageModel.ImageExtension
            };

            _context.ImageModel.Add(newImageModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImageModel", new { id = newImageModel.ImageId }, newImageModel);
        }

        [HttpGet]
        [Route("loadimages")]
        public async Task<ActionResult<IEnumerable<ImageModel>>> GetImages()
        {
            List<ImageModel> images = _context.ImageModel.ToList();
            return images;
        }

    }
}
