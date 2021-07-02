using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class GalleryImages
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public Book Book { get; set; }
    }
}
