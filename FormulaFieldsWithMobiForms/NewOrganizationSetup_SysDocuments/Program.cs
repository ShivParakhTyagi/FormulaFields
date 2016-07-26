using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.DocDBPump.Helpers.SystemForms;

namespace NewOrganizationSetup_SysDocuments
{
    class Program
    {
        static void Main(string[] args)
        {
            string terminate = "";
            do
            {
                try
                {
                    SysMobiFormHelper.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[CUSTOM]An exception was thrown :");
                    Console.WriteLine(ex.Message);
                    Debug.WriteLine(ex.Message);
                }

                Console.WriteLine("Terminate - Yes(y)");
                terminate = Console.ReadLine()?.Replace("\n", "").Replace("\r", "");
            } while (terminate != "y");
        }
    }
}
