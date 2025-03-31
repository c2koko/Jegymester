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
        //ezek a kódok (lefele) nem lesznek használva, de jobb he bent vannak
        public int RoomId { get; set; }
    }
}
