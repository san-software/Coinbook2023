using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace SAN.Plugin
{
    /// <summary>
    /// Summary description for PluginServices.
    /// </summary>
    public class PluginServiceInternal
    {
        /// <summary>
        /// Constructor of the Class
        /// </summary>
        public PluginServiceInternal(string path, string pluginName)
        {
            //Go through all the files in the plugin directory
            string file = Path.Combine(path, pluginName);

            FileInfo fileInfo = new FileInfo(file);
            Plugin = CreateInstance(file);
        }

        /// <summary>
        /// Plugins Instance Found and Loaded 
        /// </summary>
        public AvailablePlugin Plugin { get; set; }

        /// <summary>
        /// Unload and Close the AvailablePlugin
        /// </summary>
        public void ClosePlugin()
        {
            //Close all plugin instances
            //We call the plugins Dispose sub first incase it has to do Its own cleanup stuff
            Plugin.Instance.Dispose();

            //After we give the plugin a chance to tidy up, get rid of it
            Plugin.Instance = null;

            //Finally, clear our collection of available plugins
            Plugin = null;
        }

        private AvailablePlugin CreateInstance(string fileName)
        {
            AvailablePlugin plugin = null;

            //Create a new assembly from the plugin file we're adding..
            Assembly pluginAssembly = Assembly.LoadFrom(fileName);

            try
            {
                //Next we'll loop through all the Types found in the assembly
                foreach (Type pluginType in pluginAssembly.GetTypes())
                {
                    if (pluginType.IsPublic) //Only look at public types
                    {
                        if (!pluginType.IsAbstract)  //Only look at non-abstract types
                        {
                            //Gets a type object of the interface we need the plugins to match
                            Type typeInterface = pluginType.GetInterface("SAN.Plugin.IPlugin", true);

                            //Make sure the interface we want to use actually exists
                            if (typeInterface != null)
                            {
                                //Create a new available plugin since the type implements the IPlugin interface
                                plugin = new AvailablePlugin();

                                //Set the filename where we found it
                                plugin.AssemblyPath = fileName;

                                //Create a new instance and store the instance in the collection for later use
                                //We could change this later on to not load an instance.. we have 2 options
                                //1- Make one instance, and use it whenever we need it.. it's always there
                                //2- Don't make an instance, and instead make an instance whenever we use it, then close it
                                //For now we'll just make an instance of all the plugins
                                plugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));

                                //Call the initialization sub of the plugin
                                plugin.Instance.Initialize("");
                            }

                            typeInterface = null; //Mr. Clean			
                        }
                    }
                }

            }
            catch (TypeLoadException e)
            {
                var x = e;
            }
            pluginAssembly = null; //more cleanup

            return plugin;
        }
    }
}
