using System;
using System.Data;
using MySql.Data.MySqlClient;
using TestApp.Model;

namespace TestApp.Data
{
    public static class MySqlDatabase
    {
        public static int InsertTicket(int TicketId, string TicketType, string TweetId, string Description, string TicketDate, string AssignedTo, string Status)
        {
            string connStr = Constants.MySQLConnectionString;

            int noOfRowsAffected = 0;

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                
                conn.Open();

                // Perform database operations
                string storedProcName = "Insert_Ticket";

                MySqlCommand cmd = new MySqlCommand(storedProcName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@_ticketId", TicketId);
                cmd.Parameters.AddWithValue("@_ticketType", TicketType);
                cmd.Parameters.AddWithValue("@_tweetId", TweetId);
                cmd.Parameters.AddWithValue("@_description", Description);
                cmd.Parameters.AddWithValue("@_ticketDate", TicketDate);
                cmd.Parameters.AddWithValue("@_assignedTo", AssignedTo);
                cmd.Parameters.AddWithValue("@_status", Status);

                noOfRowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();

            return noOfRowsAffected;
        }

    }
}