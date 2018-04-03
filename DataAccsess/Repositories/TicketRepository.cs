using DataAccsess.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccsess.Repositories
{
    public class TicketRepository
    {
        public void InsertTicket(Ticket ticket)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "insert into tickets (Row,Seat,ShowId) Values(@1,@2,@3) ";
                SqlCommand s = new SqlCommand(query, conn);
                s.Parameters.Add("@1", SqlDbType.NVarChar).Value = ticket.Row;
                s.Parameters.Add("@2", SqlDbType.NVarChar).Value = ticket.Seat;
                s.Parameters.Add("@3", SqlDbType.NVarChar).Value = ticket.ShowId;
                s.ExecuteNonQuery();
            }
        }

        public List<Ticket> GetAllTickets()
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM tickets";
                SqlCommand S = new SqlCommand(query, conn);
                var ret = new List<Ticket>();
                var r = S.ExecuteReader();
                while (r.Read())
                {
                    ret.Add(new Ticket
                    {
                        Id = r.GetInt32(0),
                        Row = r.GetInt32(1),
                        Seat = r.GetInt32(2),
                        ShowId = r.GetInt32(3)
                    });
                }

                return ret;
            }
        }

        public Ticket GetTicketById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM TICKETS where id= @1";
                SqlCommand S = new SqlCommand(query, conn);
                S.Parameters.Add("@1", SqlDbType.Int).Value = id;
                var r = S.ExecuteReader();
                if (r.Read())
                    return new Ticket
                    {
                        Id = r.GetInt32(0),
                        Row = r.GetInt32(1),
                        Seat = r.GetInt32(2),
                        ShowId = r.GetInt32(3)
                    };
                else
                    return null;
            }
        }

        public List<Ticket> GetTicektsByShowId(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM tickets where showid=@1";
                SqlCommand S = new SqlCommand(query, conn);
                var ret = new List<Ticket>();
                S.Parameters.Add("@1", SqlDbType.Int).Value = id;
                var r = S.ExecuteReader();
                while (r.Read())
                {
                    ret.Add(new Ticket
                    {
                        Id = r.GetInt32(0),
                        Row = r.GetInt32(1),
                        Seat = r.GetInt32(2),
                        ShowId = r.GetInt32(3)
                    });
                }

                return ret;
            }
        }


        public void UpdateTicket(Ticket ticket)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "UPDATE TICKETS " +
                    "SET ROW=@2,SEAT=@3,SHOWID=@4" +
                    "where id=@1";
                SqlCommand S = new SqlCommand(query, conn);
                S.Parameters.Add("@1", SqlDbType.Int).Value = ticket.Id;
                S.Parameters.Add("@2", SqlDbType.Int).Value = ticket.Row;
                S.Parameters.Add("@3", SqlDbType.Int).Value = ticket.Seat;
                S.Parameters.Add("@4", SqlDbType.Int).Value = ticket.ShowId;
                var r = S.ExecuteNonQuery();
            }
        }

        public void DeleteTicket(int id)
        {

            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "delete from TICKETS " +
                    "where id=@1";
                SqlCommand S = new SqlCommand(query, conn);
                S.Parameters.Add("@1", SqlDbType.Int).Value = id;

                var r = S.ExecuteNonQuery();
            }
        }

        public void EmptyTable()
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "delete from TICKETS ";
                    
                SqlCommand S = new SqlCommand(query, conn);

                var r = S.ExecuteNonQuery();
            }
        }
    }
}