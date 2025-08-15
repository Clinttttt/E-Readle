using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readle.Domain.Entities
{
   public class BookStored
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string? Title { get; set; }
        public List<string>? Authors { get; set; }
        public List<string>? Languages { get; set; }
        [MaxLength]
        public string? Content { get; set; }
        public int DownloadCount { get; set; }
        public List<string>? BookShelves { get; set; }
        public List<string>? Subjects { get; set; }
    }
}
