using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace XLink.Utility
{
    /// <summary>
    /// A class to manage the application configuration
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// The instance of the AppConfiguration class
        /// </summary>
        private static AppConfiguration instance;

        /// <summary>
        /// The lock object for thread safety
        /// </summary>
        private static readonly object lockObject = new object(); 

        /// <summary>
        /// The configuration root, used to access configuration values
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// The constructor for the AppConfiguration class
        /// </summary>
        private AppConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddUserSecrets<AppConfiguration>();

            Configuration = builder.Build();
        }

        /// <summary>
        /// The instance of the AppConfiguration class
        /// </summary>
        public static AppConfiguration Instance
        {
            get
            {
                lock (lockObject)
                {
                    return instance ??= new AppConfiguration();
                }
            }
        }
    }
}
