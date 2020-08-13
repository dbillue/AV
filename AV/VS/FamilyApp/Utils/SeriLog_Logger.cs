using Serilog;
using System;

namespace FamilyApp.Utils
{
    public class SeriLog_Logger
    {
        public SeriLog_Logger() { }

        public void WriteInformation(string msg)
        {
            try
            {
                Log.Information(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void WriteError(string msg)
        {
            try
            {
                Log.Fatal(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
