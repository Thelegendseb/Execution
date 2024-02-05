using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XLink.Actions;
using XLink.Contexts.Implementations;
using XLink.Utility;

namespace XLink.Contexts
{
    /// <summary>
    /// A class to manage all the running contexts and their executions
    /// </summary>
    public class ContextManager
    {

        /// <summary>
        /// The list of contexts
        /// </summary>
        private List<Context> Contexts;

        /// <summary>
        /// The filter for the context manager
        /// </summary>
        private ContextFilter Filter;

        /// <summary>
        /// Constructor for the context manager
        /// </summary>
        public ContextManager()
        {
            Contexts = new List<Context>();
            Filter = new ContextFilter();
        }

        /// <summary>
        /// The initialization function for the context manager.
        /// </summary>
        public void Init()
        {
            LoadContexts();
        }

        /// <summary>
        /// Load all the contexts
        /// </summary>
        private void LoadContexts()
        {

            Logger.Log("Loading Contexts...", LogLevel.Info);

            LoadContext<con_Spotify>();
            LoadContext<con_Watch>();

            Logger.Log("Contexts loaded.", LogLevel.Info);

        }

        /// <summary>
        /// Execute an action.
        /// </summary>
        /// <param name="contextName">The name of the context.</param>
        /// <param name="actionName">The name of the action to execute.</param>
        /// <param name="args">The arguments to execute the action with.</param>
        /// <returns>
        /// An XActionResponse representing the response from the action.
        /// </returns>
        public XActionResponse Execute(string contextName, string actionName, string args)
        {

            Context context = Contexts.Find(x => x.GetName().ToUpper() == contextName.ToUpper());
            // Check if the context exists
            if (context == null)
            {
                Logger.Log("Context not found: " + contextName, LogLevel.Warning);
                return null;
            }

            // Check if the action exists
            if (!(context.GetActions().Keys.Any(action => action.Name == actionName)))
            {
                Logger.Log("Action not found: " + actionName + " in context: " + contextName, LogLevel.Warning);
                return null;
            }

            // Check if the action is allowed by the filter
            if (!Filter.Allows(actionName, context))
            {
                Logger.Log("Action not allowed by filter: " + actionName, LogLevel.Warning);
                return null;
            }

            return context.RunAction(actionName, args);
           
        }

        /// <summary>
        /// Attempting to add a single context to the context manager.
        /// </summary>
        /// <typeparam name="IContext">The type of context to add, must derive from Context and have a default constructor.</typeparam>
        /// <returns>
        /// A boolean value indicating whether or not the context was added successfully.
        /// </returns>
        private bool LoadContext<IContext>() where IContext : Context, new()
        {
            IContext context = new IContext();
            Logger.Log("Loading " + context.GetName() + " Context...", LogLevel.Info);
            bool init = context.Init();
            if (init)
            {
                this.Contexts.Add(context);    
                Logger.Log(context.GetName() + " Context loaded.", LogLevel.Info);
                return true;               
            }
            else
            {
                Logger.Log(context.GetName() + " Context failed to load.", LogLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// Get a list of all the context's actions and the context they belong to.
        /// </summary>
        /// <returns>
        /// A dictionary containing context names as keys and lists of action names as values.
        /// </returns>
        public Dictionary<string, List<string>> GetActions()
        {
            Dictionary<string, List<string>> actions = new Dictionary<string, List<string>>();
            foreach (Context context in Contexts)
            {
                actions.Add(context.GetName(), context.GetActions().Keys.Select(x => x.Name).ToList());
            }
            return actions;
        }

        /// <summary>
        /// Apply a filter to the context manager.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        public void ApplyFilter(ContextFilter filter)
        {
            this.Filter = filter;
            Logger.Log("Filter applied to context manager", LogLevel.Info);
        }

    }
}
