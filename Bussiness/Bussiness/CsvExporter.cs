using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Bussiness.Models;

namespace Bussiness.Bussiness
{
    public class CsvExporter : IExporter
    {
        public void ExportTickets(ShowModel show)
        {
            StringBuilder tickets = new StringBuilder();
            var title = show.Title + "_" + show.Day.ToString("dd_MM_yyyy") + ".csv";
            tickets.Append("Id,Row,Seat\n");
            foreach (var ticket in show.Tickets)
            {
                tickets.Append(ticket.Id.ToString() + "," + ticket.Row.ToString() + "," + ticket.Seat.ToString() + "\n");
            }
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"E:\Faculta\SD\projekt\WebApplication1\" + title))
            {
                file.Write(tickets.ToString());
            }
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Microsoft Office\root\Office16\excel.exe", @"E:\Faculta\SD\projekt\WebApplication1\" + title);

        }
    }
}