using Bussiness.Models;
using DataAccsess.DTO;
using DataAccsess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussiness.Mappers
{
    public static class TicketMapper
    {
        public static TicketModel AsModel(this Ticket ticket, ShowModel showModel = null)
        {
            if (ticket == null)
            {
                return null;
            }

            return new TicketModel
            {
                Id=ticket.Id,
                Row = ticket.Row,
                Seat = ticket.Seat,
                Show = showModel
            };
        }

        public static Ticket AsDto(this TicketModel ticket)
        {
            if (ticket == null)
            {
                return null;
            }

            return new Ticket
            {
                Id=ticket.Id,
                Row = ticket.Row,
                Seat = ticket.Seat,
                ShowId = ticket.Show.Id
            };
        }

    }
}