using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readle.Domain.Model
{
   public class GutendexResponse
    {
        public int Count { get; set; }
        public List<BookGutendex>? Results { get; set; }
    }
}
