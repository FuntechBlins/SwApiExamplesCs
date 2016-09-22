using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SwCommon
{
    public static class Logger
    {
        /// <summary>
        /// Logs a message to Debug and Console output.
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            Debug.WriteLine(message);
            Console.WriteLine(message);
        }
    }
}
