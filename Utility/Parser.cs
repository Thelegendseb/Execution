using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLink.Utility
{
    /// <summary>
    /// The parser for the input command
    /// </summary>
    public static class Parser
    {

        /// <summary>
        /// Parse the input command
        /// </summary>
        /// <param name="input">The input command</param>
        /// <returns>The response from the parser</returns>
        public static Query Parse(string input)
        {

            string[] split = input.Split(' ');
            if (split.Length < 2)
            {
                Logger.Log("Invalid Query Detected: " + input, LogLevel.Error);
                return null;
            }
            else
            {
                Logger.Log("Query Detected: " + input, LogLevel.Info);
                return new Query() { ContextName = split[0], ActionName = split[1], Args = string.Join(" ", split.Skip(2)) };
            }

        }

    }

    /// <summary>
    /// The input object for excecuting an action through the context manager
    /// </summary>
    public class Query
    {

        /// <summary>
        /// The name of the context
        /// </summary>
        public string ContextName { get; set; }

        /// <summary>
        /// The name of the action
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// The arguments used to run the action
        /// </summary>
        public string Args { get; set; }

    }

}
