using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BookStore.Enum;
using BookStore.Helpers;
using Microsoft.AspNetCore.Http;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter title of new book.")]
        //[MyCustomValidation(ErrorMessage = "This is a custom error message.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter author of new book.")]
        public string Author { get; set; }

        [StringLength(500, ErrorMessage = "The given description is too long.")]
        public string Description { get; set; }
        public string Category { get; set; }

        [Display(Name = "Total Pages")]
        [Required(ErrorMessage = "Please enter the total number of pages for new book.")]
        public int? TotalPages { get; set; }

        [Required(ErrorMessage = "Please choose language of book.")]
        public int LanguageId { get; set; }

        public string Language { get; set; }

        [Display(Name = "Upload book cover image")]
        public IFormFile CoverImage { get; set; }

        public string CoverImageUrl { get; set; }

        [Display(Name = "Upload book gallery images")]
        public IFormFileCollection GalleryImages { get; set; }

        //Property to retrieve gallery images from database
        public List<GalleryModel> Gallery { get; set; }

        [Display(Name = "Upload book as PDF")]
        public IFormFile PdfBook { get; set; }

        public string PdfBookUrl { get; set; }

    }
}
