using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussiness.Models
{
    public class TicketModel : BaseModel
    {
        public int Row { get; set; }
        public int Seat { get; set; }
        public ShowModel Show { get; set; }
    }
}