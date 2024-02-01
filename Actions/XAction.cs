using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLink.Actions
{
    /// <summary>
    /// Classes for all action based operations
    /// </summary>
    public abstract class XAction
    {

        /// <summary>
        /// The template for the action function
        /// </summary>
        /// <param name="query">The query to run the action with</param>
        /// <returns>An XActionResponse instance with action excecution information</returns>
        public delegate XActionResponse ResponseSchema(string query);

        /// <summary>
        /// A class to store metadata about an action
        /// </summary>
        public class RequestSchema
        { 

            /// <summary>
            /// The name of the action
            /// </summary>
             public string Name { get; set; }

            /// <summary>
            /// A flag to determine if the action returns a result
            /// </summary>
            public bool ReturnsResult { get; set; }

            /// <summary>
            /// A flag to determine if the action requires arguments
            /// </summary>
            public bool RequiresArgs { get; set; }
        
        }


    }

}
