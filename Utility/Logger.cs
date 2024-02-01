using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XLink.Actions;

namespace XLink.Utility
{

    /// <summary>`
    /// The type of log
    /// </summary>
    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    /// <summary>
    /// A class to manage logging
    /// </summary>
    public class Logger
    {

        /// <summary>
        /// Log a message to the console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Log(string message)
        {
           // change console color to white
           Console.ForegroundColor = ConsoleColor.White;
           Console.Write("[" + DateTime.Now.ToString() + "]");
           Console.Write(" [" + message + "]\n");

        }

        /// <summary>
        /// Log a message to the console with a type.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="type">The type of the log.</param>
        public static void Log(string message, LogLevel type)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[" + DateTime.Now.ToString() + "] ");
            switch (type)
            {
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[INFO]");
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[WARNING]");
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[ERROR]");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [" + message + "]\n");

        }

        /// <summary>
        /// Log an XActionResponse to the console.
        /// </summary>
        /// <param name="actionresponse">The XActionResponse to log.</param>
        public static void Log(XActionResponse actionresponse)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[" + DateTime.Now.ToString() + "] ");
            if(actionresponse.Success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[SUCCESS]");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[ERROR]");
            }
 
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [" + actionresponse + "] ");

        }

    }
}
