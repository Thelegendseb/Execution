using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLink.Actions;
using XLink.Utility;

namespace XLink.Contexts
{
    /// <summary>
    /// The base class for all contexts
    /// </summary>
    public abstract class Context
    {

        /// <summary>
        /// The name of the context
        /// </summary>
        protected string Name = "EmptyContext";

        /// <summary>
        /// The type of the context
        /// </summary>
        protected ContextType ContextType = ContextType.Misc;

        /// <summary>
        /// The actions for the context
        /// </summary>
        protected Dictionary<XAction.RequestSchema, XAction.ResponseSchema> Actions;

        /// <summary>
        /// A flag to determine if an action is running
        /// </summary>
        protected bool actionRunning = false;

        /// <summary>
        /// The configuration values for the context
        /// </summary>
        private Dictionary<string, string> ConfigValues;

        /// <summary>
        /// Initializes the context.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the context was initialized successfully.
        /// </returns>
        public abstract bool Init();

        /// <summary>
        /// Gets the names of the config values for the context.
        /// </summary>
        /// <returns>
        /// An array of strings representing the names of the config values for the context.
        /// </returns>
        protected abstract string[] GetConfigValueNames();

        /// <summary>
        /// Loads the actions for the context.
        /// </summary>
        /// <returns>
        /// A dictionary containing the actions for the context, with request and response schemas.
        /// </returns>
        protected abstract Dictionary<XAction.RequestSchema, XAction.ResponseSchema> LoadActions();

        /// <summary>
        /// Constructor for the context.
        /// </summary>
        /// <param name="name">The name of the context.</param>
        /// <param name="type">The type of the context.</param>
        public Context(string name, ContextType type)
        {
            this.Name = name;
            this.ContextType = type;
            this.ConfigValues = this.LoadConfigValues(AppConfiguration.Instance.Configuration.GetSection(this.Name));
            this.Actions = this.LoadActions();
        }

        /// <summary>
        /// Run an action from the context.
        /// </summary>
        /// <param name="actionName">The name of the action to run.</param>
        /// <param name="args">The arguments to run the action with.</param>
        /// <returns>
        /// An XActionResponse indicating the result of the action execution.
        /// </returns>
        public XActionResponse RunAction(string actionName, string args)
        {
            if (Actions.Keys.Any(action => action.Name == actionName))
            {
                XAction.ResponseSchema action = this.Actions.First(x => x.Key.Name == actionName).Value;
                this.actionRunning = true;
                XActionResponse result = action(args);
                this.actionRunning = false;
                return result;
            }
            else
            {
                return new XActionResponse(this.GetName(), actionName, args, false, "Action not found in context '" + this.Name + "'.", "");
            }
        }

        /// <summary>
        /// Load an individual config value for the context.
        /// </summary>
        /// <param name="key">The key of the config value to load.</param>
        /// <param name="config">The dictionary to store the config values for the context.</param>
        /// <param name="section">The section of the config file for the context.</param>
        protected void LoadConfigValue(string key, Dictionary<string, string> config, IConfigurationSection section)
        {
            string value = section[key];
            if (value == null)
            {
                Logger.Log("Config value '" + key + "' not found in context '" + this.Name + "'.", LogLevel.Error);
                throw new Exception("Uknown config value. See log for details");
            }
            config.Add(key, value);
        }

        /// <summary>
        /// Load the config values for the context.
        /// </summary>
        /// <param name="section">The section of the config file for the context.</param>
        /// <returns>
        /// A dictionary containing the config values for the context.
        /// </returns>
        private Dictionary<string, string> LoadConfigValues(IConfigurationSection section)
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            string[] configValueNames = this.GetConfigValueNames();
            if (configValueNames != null)
            {
                foreach (string key in configValueNames)
                {
                    this.LoadConfigValue(key, config, section);
                }
            }
            return config;
        }


        // ==================== Getters ====================

        /// <summary>
        /// Get a config value from the context.
        /// </summary>
        /// <param name="key">The key of the config value to get.</param>
        /// <returns>
        /// A string representing the requested config value.
        /// </returns>
        protected string GetConfigValue(string key)
        {
            return this.ConfigValues[key];
        }

        /// <summary>
        /// Get the name of the context.
        /// </summary>
        /// <returns>
        /// A string representing the name of the context.
        /// </returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// Get the type of the context.
        /// </summary>
        /// <returns>
        /// The type of the context as a ContextType enum.
        /// </returns>
        public ContextType GetContextType()
        {
            return this.ContextType;
        }

        /// <summary>
        /// Get the actions for the context.
        /// </summary>
        /// <returns>
        /// A dictionary containing the actions for the context, with request and response schemas.
        /// </returns>
        public Dictionary<XAction.RequestSchema, XAction.ResponseSchema> GetActions()
        {
            return this.Actions;
        }

    }

}

