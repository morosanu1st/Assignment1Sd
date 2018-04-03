using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DataAccsess.DTO;
using System.Data;

namespace DataAccsess.Repositories
{
    public class UserRepository
    {
        public void InsertUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "insert into users (UserName,FirstName,LastName,PasswordHash,IsAdmin) Values(@1,@2,@3,@4,0) ";
                SqlCommand s = new SqlCommand(query, conn);
                s.Parameters.Add("@1", SqlDbType.NVarChar).Value = user.Username;
                s.Parameters.Add("@2", SqlDbType.NVarChar).Value = user.FirstName;
                s.Parameters.Add("@3", SqlDbType.NVarChar).Value = user.LastName;
                s.Parameters.Add("@4", SqlDbType.NVarChar).Value = user.PasswordHash;
                s.ExecuteNonQuery();
            }
        }

        public List<User> GetAllUsers()
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM USERS";
                SqlCommand S = new SqlCommand(query, conn);
                var ret = new List<User>();
                var r = S.ExecuteReader();
                while (r.Read())
                {
                    ret.Add(new User
                    {
                        Id = r.GetInt32(0),
                        Username = r.GetString(1),
                        FirstName = r.GetString(2),
                        LastName = r.GetString(3),
                        PasswordHash = r.GetString(4),
                        IsAdmin = r.GetBoolean(5)

                    });
                }

                return ret;
            }
        }

        public User GetUserById(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM USERS where id= @1";
                SqlCommand S = new SqlCommand(query, conn);
                S.Parameters.Add("@1", SqlDbType.Int).Value = id;
                var r = S.ExecuteReader();
                if (r.Read())
                    return new User
                    {
                        Id = r.GetInt32(0),
                        Username = r.GetString(1),
                        FirstName = r.GetString(2),
                        LastName = r.GetString(3),
                        PasswordHash = r.GetString(4),
                        IsAdmin = r.GetBoolean(5)

                    };
                else
                    return null;
            }
        }

        public User GetUserByUserName(string userName)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "SELECT * FROM USERS where USERNAME= @1";
                SqlCommand S = new SqlCommand(query, conn);
                S.Parameters.Add("@1", SqlDbType.NVarChar).Value = userName;
                var r = S.ExecuteReader();
                if (r.Read())
                    return new User
                    {
                        Id = r.GetInt32(0),
                        Username = r.GetString(1),
                        FirstName = r.GetString(2),
                        LastName = r.GetString(3),
                        PasswordHash = r.GetString(4),
                        IsAdmin = r.GetBoolean(5)

                    };
                else
                    return null;
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "UPDATE USERS " +
                    "SET UserName=@2,FIRSTNAME=@3,LASTNAME=@4, PASSWORDHASH=@5 " +
                    "where id=@1";
                SqlCommand S = new SqlCommand(query, conn);
                S.Parameters.Add("@1", SqlDbType.Int).Value = user.Id;
                S.Parameters.Add("@2", SqlDbType.NVarChar).Value = user.Username;
                S.Parameters.Add("@3", SqlDbType.NVarChar).Value = user.FirstName;
                S.Parameters.Add("@4", SqlDbType.NVarChar).Value = user.LastName;
                S.Parameters.Add("@5", SqlDbType.NVarChar).Value = user.PasswordHash;
                var r = S.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {

            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                var query = "delete from USERS " +
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
                var query = "delete from USERS";

                SqlCommand S = new SqlCommand(query, conn);

                var r = S.ExecuteNonQuery();
            }
        }

    }
}