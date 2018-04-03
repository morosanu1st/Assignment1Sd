using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccsess.DTO
{
    public class Show : Entity
    {
        public string Title { get; set; }
        public string Distribution { get; set; }
        public int Genre { get;set; }
        public DateTime Day { get; set; }
        public int Seats { get; set; }
    }
}