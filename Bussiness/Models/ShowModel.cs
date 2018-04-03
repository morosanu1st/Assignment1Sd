using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussiness.Models
{
    public class ShowModel : BaseModel
    {
        public string Title { get; set; }
        public string Distribution { get; set; }
        public Genre Genre { get; set; }
        public DateTime Day { get; set; }
        public int Seats { get; set; }
        public List<TicketModel> Tickets { get; set; }
    }

    public enum Genre { Play,Opera,Ballet,Concert}
}