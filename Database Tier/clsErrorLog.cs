using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Database_Tier
{
    public class clsErrorLog
    {

        public static void Log(string message)
        {
            // Specify the source name for the event log
            string sourceName = "Library Management System (LMS)";


            // Create the event source if it does not exist
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }

            // Log an information event
            EventLog.WriteEntry(sourceName, message, EventLogEntryType.Error);

        }

    }
}
