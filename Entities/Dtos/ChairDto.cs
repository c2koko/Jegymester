using Jegymester.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.DataContext.Dtos
{
    public class ChairDto // ne legyen auto incrementálás !!!
    {
        public int Id { get; set; }
        public bool IsReserved { get; set; }

        public ChairDto(Chair chair) { Id = chair.Id; IsReserved = chair.IsReserved; } // Konstruktor
        public ChairDto() { } // Üres konstruktor
    }
}
