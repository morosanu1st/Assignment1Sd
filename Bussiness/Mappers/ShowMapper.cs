using Bussiness.Models;
using Bussiness.Services;
using DataAccsess.DTO;
using DataAccsess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussiness.Mappers
{
    public static class ShowMapper
    {
        private static TicketRepository _ticketRepository = new TicketRepository();

        public static ShowModel AsModel(this Show show)
        {
            if (show == null)
            {
                return null;
            }

            var showModel = new ShowModel
            {
                Id=show.Id,
                Title = show.Title,
                Distribution = show.Distribution,
                Genre = (Genre)show.Genre,
                Day = show.Day,
                Seats=show.Seats
            };

            showModel.Tickets = _ticketRepository.GetTicektsByShowId(show.Id).Select(ticket => ticket.AsModel(showModel)).ToList();

            return showModel;
        }

        public static Show AsDto(this ShowModel show)
        {
            if (show == null)
            {
                return null;
            }

            return new Show
            {
                Id = show.Id,
                Title = show.Title,
                Distribution = show.Distribution,
                Genre = (int)show.Genre,
                Day = show.Day,
                Seats=show.Seats
            };
        }
    }
}