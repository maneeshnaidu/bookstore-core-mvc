using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    CoverImageUrl = book.CoverImageUrl,
                    PdfBookUrl = book.PdfBookUrl

                }).ToListAsync();


        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Category = model.Category,
                Description = model.Description,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                LanguageId = model.LanguageId,
                CoverImageUrl = model.CoverImageUrl,
                PdfBookUrl = model.PdfBookUrl,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            newBook.GalleryImages = new List<GalleryImages>();

            //Uploading multiple files into the database
            foreach (var file in model.Gallery)
            {
                newBook.GalleryImages.Add(new GalleryImages()
                {
                    Name = file.Name,
                    Url = file.Url
                });

            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(b => b.Id == id)
                .Select(book => new BookModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    TotalPages = book.TotalPages,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    CoverImageUrl = book.CoverImageUrl,
                    PdfBookUrl = book.PdfBookUrl,
                    Gallery = book.GalleryImages.Select(file => new GalleryModel()
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Url = file.Url
                    }).ToList()
                }).FirstOrDefaultAsync();


        }

        public List<BookModel> SearchBook(string title, string author)
        {
            var bookList = new List<BookModel>();

            var list = _context.Books.Where(b => b.Title.Contains(title) || b.Author.Contains(author)).ToList();

            if (list?.Any() == true)
            {
                foreach (var book in list)
                {
                    bookList.Add(new BookModel()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        TotalPages = book.TotalPages,
                        LanguageId = book.LanguageId,
                    });
                }
            }

            return bookList;
        }

        public async Task<List<BookModel>> GetTopBooks(int count)
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    CoverImageUrl = book.CoverImageUrl,
                    PdfBookUrl = book.PdfBookUrl

                }).Take(count).ToListAsync();


        }


    }
}
