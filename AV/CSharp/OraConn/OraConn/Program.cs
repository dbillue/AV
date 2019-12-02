using System;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace OraConn
{
    class Program
    {
        static string oConnection = ConfigurationManager.AppSettings.Get("oraconn");

        static void Main(string[] args)
        {
            try
            {
                
                Console.WriteLine(oConnection);
                Console.ReadLine();
                using OracleConnection oConn = new OracleConnection(oConnection);
                oConn.Open();
                Console.WriteLine("Connection status {0}.", oConn.State);
                Console.WriteLine("Hello World!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
