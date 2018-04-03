using Bussiness.Bussiness;
using Bussiness.Mappers;
using Bussiness.Models;
using DataAccsess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussiness.Services
{
    public class ShowService
    {
        private ShowRepository _showRepository;
        private TicketRepository _ticketRepository;
        private IExporter _exporter;
        private ExporterFactory _exporterFactory;

        public ShowService()
        {
            _showRepository = new ShowRepository();
            _ticketRepository = new TicketRepository();
            _exporterFactory = new ExporterFactory();
        }

        public List<ShowModel> GetAllShows()
        {
            return _showRepository.GetAllShows().Select(show => show.AsModel()).ToList();
        }

        public ShowModel GetShowById(int id)
        {
            return _showRepository.GetShowById(id).AsModel();
        }

        public List<ShowModel> GetShowByTitle(string title)
        {
            return _showRepository.GetShowByTitle(title).Select(show => show.AsModel()).ToList();
        }

        public ShowModel GetShowByDay(DateTime day)
        {
            return _showRepository.GetShowByDay(day).AsModel();
        }

        public bool CreateShow(ShowModel show)
        {
            if (show == null || show.Day == null || _showRepository.GetShowByDay(show.Day) != null || show.Title == null || show.Seats == 0 || show.Seats > Constants.Rows * Constants.Seats)
            {

                return false;
            }

            _showRepository.InsertShow(show.AsDto());

            return true;
        }

        public int TicketsLeftForShow(ShowModel show)
        {
            var s = GetShowByDay(show.Day);

            return s.Seats - (s?.Tickets.Count() ?? 0);
        }

        public bool IsShowSoldOut(ShowModel show)
        {
            return TicketsLeftForShow(show) == 0;
        }

        public bool ExportShow(ShowModel show, string extension = "xml")
        {
            _exporter = _exporterFactory.GetExporter(extension);
            if (_exporter == null)
            {

                return false;
            }
            _exporter.ExportTickets(show);

            return true;
        }

        public bool UpdateShow(ShowModel show)
        {
            var toUpdate = _showRepository.GetShowById(show.Id);
            if (show == null || show.Day == null || show.Seats > Constants.Rows * Constants.Seats)
            {

                return false;
            }

            if (show.Day > DateTime.Now)
            {
                var v = GetShowByDay(show.Day);
                if (v != null && v.Id != toUpdate.Id)
                {
                    return false;
                }
            }
            else
            {
                show.Day = toUpdate.Day;
            }

            show.Title = show.Title ?? toUpdate.Title;
            show.Distribution = show.Distribution ?? toUpdate.Distribution;
            show.Seats = show.Seats == 0 ? toUpdate.Seats : show.Seats;
            

            _showRepository.UpdateShow(show.AsDto());

            return true;
        }

        public bool DeleteShow(ShowModel show)
        {
            show = GetShowById(show.Id);
            if (show == null || show.Id == 0 || _showRepository.GetShowById(show.Id) == null)
            {
                return false;
            }

            foreach (var ticket in show.Tickets)
            {
                try
                {
                    _ticketRepository.DeleteTicket(ticket.Id);
                }
                catch
                {
                    return false;
                }
            }

            _showRepository.DeleteShow(show.Id);

            return true;
        }
    }
}