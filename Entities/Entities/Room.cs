﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegymester.DataContext.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public List<Screening> screening { get; set; } // adatbázisban nincs
        public List<Chair> chairs { get; set; }
    }
}
