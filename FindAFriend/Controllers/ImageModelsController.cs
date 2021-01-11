using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FindAFriend.Data;
using FindAFriend.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FindAFriend.Controllers
{
    public class ImageModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        

        public ImageModelsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        
        public IActionResult Create()
        {
            ImageModel oldProfilepic = _context.ImageModel.FirstOrDefault(i => i.UserEmail == User.Identity.Name + i.ImageExtension);
            if (oldProfilepic != null)
            {
                _context.ImageModel.Remove(oldProfilepic);
                ViewData["extension"] = oldProfilepic.ImageExtension;
            }
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId, ImageFile")] ImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                ImageModel oldProfilepic = _context.ImageModel.FirstOrDefault(i => i.UserEmail == User.Identity.Name + i.ImageExtension);
                if (oldProfilepic != null)
                {
                    _context.ImageModel.Remove(oldProfilepic);
                    var fileLocation = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", oldProfilepic.UserEmail);
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                }
                try
                {
                    //Sparar en bild i wwwRoot/Images
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string userEmail = User.Identity.Name;
                    if(imageModel.ImageFile != null)
                    {
                        string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                        imageModel.ImageExtension = extension;
                        imageModel.UserEmail = userEmail = userEmail + extension;
                        string path = Path.Combine(wwwRootPath + "/Images/", userEmail);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await imageModel.ImageFile.CopyToAsync(fileStream);
                        }

                        _context.Add(imageModel);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Create));
                    }
                    else
                    {
                        return RedirectToAction(nameof(Create));
                    }
                }

                catch(Exception e)
                {
                    return RedirectToAction(nameof(Create));
                }
            }
            return View(imageModel);
        }
    }
}
