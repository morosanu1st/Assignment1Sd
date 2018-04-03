using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccsess.DTO
{
    public class Ticket : Entity
    {
        public int ShowId { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
    }
}