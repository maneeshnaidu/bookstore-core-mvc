using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            var data = await _bookRepository.GetAllBooks();

            return View(data);
        }

        [Route("~/book-details/{id:int:min(1)}", Name = "book-details")]
        public async Task<IActionResult> Detail(int id)
        {
            var data = new BookModel() { Id = 1, Author = "Anonymous", Title = "Auto Book", Category = "Programming", Description = "This is a sample description.", LanguageId = 1, TotalPages = 12345 }; 

            if (id != 0)
            {
                data = await _bookRepository.GetBookById(id);

                return View(data);
            }
 

            return View(data);           

            
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create(bool isSuccess = false, int bookId = 0)
        {
            //ViewBag.Language = new SelectList(await _languageRepository.GetAllLanguages(), "Id", "Name");

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Create(BookModel model)
        {
            if (ModelState.IsValid)
            {
                //If PDF file is availabel for upload
                if(model.PdfBook != null)
                {
                    string folder = "books/pdf/";
                    model.PdfBookUrl = await UploadFile(folder, model.PdfBook);
                }
                //If cover image is available for upload
                if(model.CoverImage != null)
                {
                    string folder = "books/cover/";
                    model.CoverImageUrl = await UploadFile(folder, model.CoverImage);
                }

                if (model.GalleryImages != null)
                {
                    string folder = "books/gallery/";

                    model.Gallery = new List<GalleryModel>();

                    foreach (var file in model.GalleryImages)
                    {
                        var gallery = new GalleryModel() 
                        { 
                            Name = file.FileName,
                            Url = await UploadFile(folder, file)
                        };
                        model.Gallery.Add(gallery);
                    }
                    model.CoverImageUrl = await UploadFile(folder, model.CoverImage);
                }

                int id = await _bookRepository.AddNewBook(model);

                if (id > 0)
                {
                    return RedirectToAction(nameof(Create), new { isSuccess = true, bookId = id });
                }
            }

            //ViewBag.Language = new SelectList(await _languageRepository.GetAllLanguages(), "Id", "Name");

            //Send custome error message to validation summary
            ModelState.AddModelError("", "This is a custom error message");

            
            return View();
        }

        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {            
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
 
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));


            return "/" + folderPath;
        }

    }
}
