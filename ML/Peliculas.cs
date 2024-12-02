using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ML
{
    public class Peliculas
    {
        public string? backdrop_path { get; set; }
        public int id { get; set; }
        public string? original_language { get; set; }
        public string? original_title { get; set; }
        public string? overview { get; set; }
        public string? poster_path { get; set; }
        public string? title { get; set; }
    }
}
