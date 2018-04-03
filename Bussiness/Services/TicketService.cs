using Bussiness.Mappers;
using Bussiness.Models;
using DataAccsess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussiness.Services
{
    public class TicketService
    {
        private TicketRepository _ticketRepository;
        private ShowService _showService;

        public TicketService()
        {
            _ticketRepository = new TicketRepository();
            _showService = new ShowService();
        }

        public List<TicketModel> GetTicketsByShowId(int id)
        {
            var v = _showService.GetShowById(id);
            if (v != null)
            {

                return _showService.GetShowById(id).Tickets;
            }
            else
            {

                return _ticketRepository.GetTicektsByShowId(id).Select(t => t.AsModel()).ToList();
            }
        }

        public TicketModel GetTicketByPlace(int showId, int row, int seat)
        {

            return _showService.GetShowById(showId)?.Tickets.Where(ticket => ticket.Row == row && ticket.Seat == seat).FirstOrDefault();
        }

        public TicketModel GetTicketById(int id)
        {
            var ticketDTO = _ticketRepository.GetTicketById(id);
            if (ticketDTO == null)
            {

                return null;
            }

            return _showService.GetShowById(ticketDTO.ShowId)?.Tickets.Where(ticket => ticket.Id == id).FirstOrDefault();
        }

        public bool CreateTicket(TicketModel ticket)
        {
            if (ticket.Row < 1 || ticket.Row >= Constants.Rows|| ticket.Seat < 1 || ticket.Seat >= Constants.Seats)
            {

                return false;
            }
            if (GetTicketByPlace(ticket.Show.Id, ticket.Row, ticket.Seat) != null || _showService.GetShowById(ticket.Show.Id) == null)
            {

                return false;
            }
            if (_showService.IsShowSoldOut(ticket.Show))
            {

                return false;
            }

            _ticketRepository.InsertTicket(ticket.AsDto());

            return true;
        }

        public bool EditSeat(TicketModel ticket)
        {
            var toUpdate = GetTicketById(ticket.Id);

            if (ticket.Row < 1 || ticket.Row >= Constants.Rows || ticket.Seat < 1 || ticket.Seat >= Constants.Seats)
            {

                return false;
            }

            if (GetTicketByPlace(ticket.Show.Id, ticket.Row, ticket.Seat) != null || _showService.GetShowById(ticket.Show.Id) == null || toUpdate==null)
            {

                return false;
            }

            toUpdate.Seat = ticket.Seat;
            toUpdate.Row = ticket.Row;

            _ticketRepository.UpdateTicket(toUpdate.AsDto());

            return true;
        }

        public bool DeleteTicket(TicketModel ticket)
        {
            try
            {
                _ticketRepository.DeleteTicket(ticket.Id);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}