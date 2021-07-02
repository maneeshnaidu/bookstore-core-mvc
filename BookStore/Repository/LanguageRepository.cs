using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreContext _context = null;

        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageModel>> GetAllLanguages()
        {
            //Initiate list for model
            var languages = new List<LanguageModel>();

            //Get list of all records from database.
            var allLanguages = await _context.Languages.ToListAsync();

            //Check if there are any records in the database
            if (allLanguages?.Any() == true)
            {
                foreach (var item in allLanguages)
                {
                    languages.Add(new LanguageModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                    });
                }
            }

            return languages;

        }
    }
}
