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

        // GET: ImageModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImageModel.ToListAsync());
        }

        // GET: ImageModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.ImageModel
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // GET: ImageModels/Create
        public IActionResult Create()
        {
            ImageModel oldProfilepic = _context.ImageModel.FirstOrDefault(i => i.UserEmail == User.Identity.Name + i.ImageExtension);
            if (oldProfilepic != null)
            {
                _context.ImageModel.Remove(oldProfilepic);
                ViewData["extension"] = oldProfilepic.ImageExtension;
            }
            

            //ViewData["extension"] = TempData["extension"];
            return View();
        }

        // POST: ImageModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                //Sparar en bild i wwwRoot/Images
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string userEmail = User.Identity.Name;
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
            return View(imageModel);
        }

        // GET: ImageModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.ImageModel.FindAsync(id);
            if (imageModel == null)
            {
                return NotFound();
            }
            return View(imageModel);
        }

        // POST: ImageModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,UserEmail,ImageExtension")] ImageModel imageModel)
        {
            if (id != imageModel.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageModelExists(imageModel.ImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(imageModel);
        }

        // GET: ImageModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.ImageModel
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // POST: ImageModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageModel = await _context.ImageModel.FindAsync(id);
            _context.ImageModel.Remove(imageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageModelExists(int id)
        {
            return _context.ImageModel.Any(e => e.ImageId == id);
        }
        
        //KOLLA SÅ VACKER!!
        public string GetFileExtension(string profileID)
        {
            ImageModel[] imageList = _context.ImageModel.ToArray();
            string iExtension = "";
            foreach (ImageModel image in imageList)
            {
                if (image.UserEmail.Contains(profileID))
                {
                    iExtension = image.ImageExtension;
                    break;
                }
            }
            return iExtension;
        }

        //FUNKAR INTE, MEN VORE NICE OM DEN GJORDE DET! - RETURNAR ALLTID FALSE
        public static string DefaultProfile(string CurrentPicPath)
        {
            string ProfilePicPath;
            var fileLocation = "https://localhost:44335/wwwroot/Images/" + CurrentPicPath;
            if (System.IO.File.Exists(fileLocation))
            {
                ProfilePicPath = CurrentPicPath;
            }
            else
            {
                ProfilePicPath = "~/Images/default.jpg";
            }
            return ProfilePicPath;
        }
    }
}
