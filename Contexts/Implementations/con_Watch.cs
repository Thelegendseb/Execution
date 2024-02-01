using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using XLink.Utility;
using XLink.Actions;

namespace XLink.Contexts.Implementations
{
    class con_Watch : Context
    {

        public con_Watch() : base("Watch", ContextType.Tool)
        { }

        public override bool Init()
        {
            // Watch has no initialization
            return true;
        }

        protected override string[] GetConfigValueNames()
        {
            // Watch will not have any config values
            return new string[] { };
        }

        protected override Dictionary<XAction.RequestSchema, XAction.ResponseSchema> LoadActions()
        {

            var actions = new Dictionary<XAction.RequestSchema, XAction.ResponseSchema>();
            actions[new XAction.RequestSchema()
            { Name = "date", ReturnsResult = true, RequiresArgs = false }] = DateAction;
            actions[new XAction.RequestSchema()
            { Name = "time", ReturnsResult = true, RequiresArgs = false }] = TimeAction;
            actions[new XAction.RequestSchema()
            { Name = "verbosetime", ReturnsResult = true, RequiresArgs = false }] = VerboseTimeAction;
            return actions;

        }

        // ==================== Actions ====================

        private XActionResponse DateAction(string args)
        {
            try
            {
                return new XActionResponse(this.GetName(), "date", args, true, null, DateTime.Now.ToString("dd/MM/yyyy"));
            }
            catch (Exception ex)
            {
                Logger.Log("Error Getting Date: " + ex.Message, LogLevel.Error);
                return new XActionResponse(this.GetName(), "date", args, false, "Error Getting Date: " + ex.Message, null);
            }
        }

        private XActionResponse TimeAction(string args)
        {
            try
            {
                return new XActionResponse(this.GetName(), "time", args, true, null, DateTime.Now.ToString("HH:mm"));
            }
            catch (Exception ex)
            {
                Logger.Log("Error Getting Time: " + ex.Message, LogLevel.Error);
                return new XActionResponse(this.GetName(), "time", args, false, "Error Getting Time: " + ex.Message, null);
            }
        }

        private XActionResponse VerboseTimeAction(string args)
        {
            try
            {
                string time = GetVerboseString();
                return new XActionResponse(this.GetName(), "verbosetime", args, true, null, time);
            }
            catch (Exception ex)
            {
                Logger.Log("Error Getting Verbose Time: " + ex.Message, LogLevel.Error);
                return new XActionResponse(this.GetName(), "verbosetime", args, false, "Error Getting Verbose Time: " + ex.Message, null);
            }
        }


        // ==================== Context Helpers ====================

        /// <summary>
        /// Generates a verbose string representation of the current time.
        /// </summary>
        /// <returns>
        /// A string representing the current time in a verbose format.
        /// </returns>
        private string GetVerboseString()
        {
            
            DateTime time = DateTime.Now;
            int hour = time.Hour;
            int minute = time.Minute;

            if(hour > 12)
            {
                hour -= 12;
            }

            int min_leeway = 5;
            int half_leeway = (int)Math.Ceiling((double)min_leeway / 2);

           if(minute <= min_leeway)
           {
                if(hour == 0)
                {
                    return "It is midnight";
                }
                return "It is " + hour + " o'clock";
           }
           if(minute >= 60 - min_leeway)
           {
                int next_hour = hour + 1;
                if(next_hour == 24)
                {
                    return "It is midnight";
                }
                return "It is " + (hour + 1) + " o'clock";
           }

           if(minute >= 30 - min_leeway && minute <= 30 + min_leeway)
            {
                return "It is half past " + hour;
           }       

           if(minute >= 15 - half_leeway && minute <= 15 + half_leeway)
           {
                if (hour == 0)
                {
                    return "It is quarter past midnight";
                }
                return "It is quarter past " + hour;
           }
           if(minute >= 45 - half_leeway && minute <= 45 + half_leeway)
           {
               if(hour == 23)
               {
                   return "It is quarter to midnight";
               }
               return "It is quarter to " + (hour + 1);
           }

            return "It is " + DateTime.Now.ToString("HH:mm");

        }


    }
}
