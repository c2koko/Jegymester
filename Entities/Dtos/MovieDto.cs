using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.DataContext.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string MovieDescrition { get; set; }
    }

    public class MovieUpdateDto
    {
        public string? MovieName { get; set; }
        public string MovieDescrition { get; set; }
    }

    public class MovieCreateDto
    {
        public string MovieName { get; set; }
        public string MovieDescrition { get; set; }
    }
}
