using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XLink.Actions;
using XLink.Contexts;
using XLink.Contexts.Implementations;
using XLink.Utility;

namespace XLink.Contexts
{
    // summary: A class to manage all the running contexts and their executions
    public class ContextManager
    {

        // summary: The list of contexts
        private List<Context> Contexts;

        // summary: Constructor for the context manager
        public ContextManager()
        {
            Contexts = new List<Context>();
        }

        // summary: The initialization function for the context manager
        public void Init()
        {
            LoadContexts();
        }

        // summary: Load all the contexts
        private void LoadContexts()
        {

            Logger.Log("Loading Contexts...", LogLevel.Info);

            LoadContext<con_Spotify>();
            LoadContext<con_Watch>();

            Logger.Log("Contexts loaded.", LogLevel.Info);

        }

        // summary: Execute an action
        // param: string actionName - the name of the action to execute
        // param: string args - the args to execute the action with
        // returns: XActionResponse - the response from the action
        public XActionResponse Execute(string contextName, string actionName, string args)
        {

            Context context = Contexts.Find(x => x.GetName().ToUpper() == contextName.ToUpper());
            if (context == null)
            {
                Logger.Log("Context not found: " + contextName, LogLevel.Info);
                return null;
            }
            else
            {
                return context.RunAction(actionName, args);
            }

        }

        // summary: attempting to add a single context to the context manager
        // param: TContext - the context to add
        // returns: bool - whether or not the context was added
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

        // summary: get a list of all the contexts's actions and the context they belong to
        // returns: Dictionary<string,List<string>> - the list of contexts and their actions
        public Dictionary<string, List<string>> GetActions()
        {
            Dictionary<string, List<string>> actions = new Dictionary<string, List<string>>();
            foreach (Context context in Contexts)
            {
                actions.Add(context.GetName(), context.GetActions().Keys.Select(x => x.Name).ToList());
            }
            return actions;
        }

    }
}
