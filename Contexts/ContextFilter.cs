using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLink.Actions;

namespace XLink.Contexts
{
    /// <summary>
    /// The filter for the context manager
    /// </summary>
    public class ContextFilter
    {

        /// <summary>
        /// A flag dictating if we can provide arguements to the context
        /// </summary>
        public bool ArgEntry { get; set; }

        /// <summary>
        /// A flag dictating if we want to run actions that return a result
        /// </summary>
        public bool ResultAcceptance { get; set; }

        /// <summary>
        /// The constructor for the context filter
        /// </summary>
        public ContextFilter()
        {
            ArgEntry = true;
            ResultAcceptance = true;
        }

        /// <summary>
        /// If the filter allows a certian action or not
        /// </summary>
        /// <param name="actionName">The name of the action</param>
        /// <param name="context">The context to check</param>
        /// <returns>A boolean value indicating if the action is allowed</returns>
        public bool Allows(string actionName, Context context)
        {
            Dictionary<XAction.RequestSchema, XAction.ResponseSchema> actions = context.GetActions();
            XAction.RequestSchema action = actions.Keys.Where(a => a.Name == actionName).First();
            if (action.RequiresArgs == true && !ArgEntry)
            {
                return false;
            }
            if (action.ReturnsResult == true && !ResultAcceptance)
            {
                return false;
            }
            return true;
        }
    }
}
