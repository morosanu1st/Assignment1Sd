using DataAccsess.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccsess.Repositories
{
    public class ShowRepository
    {
        public void InsertShow(Show show)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "insert into SHOWS (Title,Distribution,Genre,Day,Seats) Values(@1,@2,@3,@4,@5) ";
                SqlCommand s = new SqlCommand(query, conn);
                s.Parameters.Add("@1", SqlDbType.NVarChar).Value = show.Title;
                s.Parameters.Add("@2", SqlDbType.NVarChar).Value = show.Distribution;
                s.Parameters.Add("@3", SqlDbType.Int).Value = show.Genre;
                s.Parameters.Add("@4", SqlDbType.Date).Value = show.Day;
                s.Parameters.Add("@5", SqlDbType.Int).Value = show.Seats;
                s.ExecuteNonQuery();
            }
        }

        public List<Show> GetAllShows()
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM SHOWS";
                SqlCommand S = new SqlCommand(query, conn);
                var ret = new List<Show>();
                var r = S.ExecuteReader();
                while (r.Read())
                {
                    ret.Add(new Show
                    {
                        Id = r.GetInt32(0),
                        Title = r.GetString(1),
                        Distribution = r.GetString(2),
                        Genre = r.GetInt32(3),
                        Day = r.GetDateTime(4),
                        Seats=r.GetInt32(5)
                    });
                }

                return ret;
            }
        }

        public Show GetShowById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM SHOWS where id= @1";
                SqlCommand S = new SqlCommand(query, conn);
                S.Parameters.Add("@1", SqlDbType.Int).Value = id;
                var r = S.ExecuteReader();
                if (r.Read())
                    return new Show
                    {
                        Id = r.GetInt32(0),
                        Title = r.GetString(1),
                        Distribution = r.GetString(2),
                        Genre = r.GetInt32(3),
                        Day = r.GetDateTime(4),
                        Seats = r.GetInt32(5)
                    };
                else
                    return null;
            }
        }

        public List<Show> GetShowByTitle(string title)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM SHOWS where title= @1";
                SqlCommand S = new SqlCommand(query, conn);
                S.Parameters.Add("@1", SqlDbType.NVarChar).Value = title;
                var r = S.ExecuteReader();
                var shows = new List<Show>();
                while (r.Read())
                    shows.Add(new Show
                    {
                        Id = r.GetInt32(0),
                        Title = r.GetString(1),
                        Distribution = r.GetString(2),
                        Genre = r.GetInt32(3),
                        Day = r.GetDateTime(4),
                        Seats = r.GetInt32(5)
                    });

                return shows;
            }
        }

        public Show GetShowByDay(DateTime day)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM SHOWS where day=@1";
                SqlCommand S = new SqlCommand(query, conn);
                S.Parameters.Add("@1", SqlDbType.NVarChar).Value = day.Date;
                var r = S.ExecuteReader();
                if (r.Read())
                    return new Show
                    {
                        Id = r.GetInt32(0),
                        Title = r.GetString(1),
                        Distribution = r.GetString(2),
                        Genre = r.GetInt32(3),
                        Day = r.GetDateTime(4),
                        Seats = r.GetInt32(5)
                    };
                else
                    return null;
            }
        }

        public void UpdateShow(Show show)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "UPDATE SHOWS " +
                    "SET TITLE=@2,DISTRIBUTION=@3,GENRE=@4,DAY=@5,SEATS=@6 " +
                    "where id=@1";
                SqlCommand s = new SqlCommand(query, conn);
                s.Parameters.Add("@1", SqlDbType.Int).Value = show.Id;
                s.Parameters.Add("@2", SqlDbType.NVarChar).Value = show.Title;
                s.Parameters.Add("@3", SqlDbType.NVarChar).Value = show.Distribution;
                s.Parameters.Add("@4", SqlDbType.Int).Value = show.Genre;
                s.Parameters.Add("@5", SqlDbType.DateTime).Value = show.Day;
                s.Parameters.Add("@6", SqlDbType.Int).Value = show.Seats;
                var r = s.ExecuteNonQuery();
            }
        }

        public void DeleteShow(int id)
        {

            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "delete from SHOWS " +
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
                var query = "delete from SHOWS ";

                SqlCommand S = new SqlCommand(query, conn);

                var r = S.ExecuteNonQuery();
            }
        }
    }
}