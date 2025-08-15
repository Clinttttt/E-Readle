using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Readle.Domain.Model
{
  public  class BookGutendex
    {
        public int Id { get; set; }
        public string? title { get; set; }
       
        [JsonPropertyName("Languages")]
        public List<string>? Languages { get; set; }
        [JsonPropertyName("Authors")]
        public List<Authors>? Author { get; set; }

        [JsonPropertyName("formats")]
        public Dictionary<string, string>? Format { get; set; }
        public class Authors 
        {
               public string? Name { get; set; }
              public int? Birth_Year { get; set; }
              public int? Death_Year { get; set; }
      }
    }
}
