using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLink.Actions
{
    /// <summary>
    /// The response from an action
    /// </summary>
    public class XActionResponse
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

        /// <summary>
        /// The result of the action
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Whether or not the action was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The error message if the action was not successful
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The constructor for the XActionResponse class
        /// </summary>
        /// <param name="contextname">The name of the context</param>
        /// <param name="actionname">The name of the action</param>
        /// <param name="args">The arguments used to run the action</param>
        /// <param name="success">Whether or not the action was successful</param>
        /// <param name="errorMessage">The error message if the action was not successful</param>
        /// <param name="result">The result of the action</param>
        public XActionResponse(string contextname,
                              string actionname,
                              string args,
                              bool success,
                              string errorMessage,
                              string result = "This action does not return a result.")
        {
            this.ContextName = contextname;
            this.ActionName = actionname;
            this.Args = args;
            this.Result = result;
            this.Success = success;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Returns a string representation of the XActionResponse
        /// </summary>
        public override string ToString()
        {
            return $"Context: {this.ContextName} | Action: {this.ActionName} | Args: {this.Args} | Result: {this.Result} | Success: {this.Success} | ErrorMessage: {this.ErrorMessage}";
        }


    }
}
