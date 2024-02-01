using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLink.Utility
{
    // summary: The parser for the input command
    public static class Parser
    {

        // summary: Parse the input command
        // param: string input - the input command
        // returns: Query - the response from the parser
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

    // summary: The response from the parser
    public class Query
    {
        // summary: The name of the context
        public string ContextName { get; set; }

        // summary: The name of the action
        public string ActionName { get; set; }

        // summary: The arguments used to run the action
        public string Args { get; set; }

    }

}
