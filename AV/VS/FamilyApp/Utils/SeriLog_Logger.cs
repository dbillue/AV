using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
