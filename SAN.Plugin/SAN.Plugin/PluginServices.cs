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
    public class PluginServices
    {
        private AvailablePlugins availablePlugins = new AvailablePlugins();

        /// <summary>
        /// Constructor of the Class
        /// </summary>
        public PluginServices(string path)
        {
            //path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            findPlugins(path);
        }

        /// <summary>
        /// A Collection of all Plugins Found and Loaded by the FindPlugins() Method
        /// </summary>
        public AvailablePlugins AvailablePlugins
        {
            get { return availablePlugins; }
            set { availablePlugins = value; }
        }

        /// <summary>
        /// Searches the passed Path for Plugins
        /// </summary>
        /// <param name="path">Directory to search for Plugins in</param>
        private void findPlugins(string path)
        {
            //First empty the collection, we're reloading them all
            availablePlugins.Clear();

            //Go through all the files in the plugin directory
            string[] files = Directory.GetFiles(path, "CloudBackup.dll");
            foreach (string fileName in files)
            {
                FileInfo file = new FileInfo(fileName);
                AddPlugin(fileName);
            }
        }

        /// <summary>
        /// Unloads and Closes all AvailablePlugins
        /// </summary>
        public void ClosePlugins()
        {
            foreach (AvailablePlugin pluginOn in availablePlugins)
            {
                //Close all plugin instances
                //We call the plugins Dispose sub first incase it has to do Its own cleanup stuff
                pluginOn.Instance.Dispose();

                //After we give the plugin a chance to tidy up, get rid of it
                pluginOn.Instance = null;
            }

            //Finally, clear our collection of available plugins
            availablePlugins.Clear();
        }

        private void AddPlugin(string fileName)
        {
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
                                AvailablePlugin newPlugin = new AvailablePlugin();

                                //Set the filename where we found it
                                newPlugin.AssemblyPath = fileName;

                                //Create a new instance and store the instance in the collection for later use
                                //We could change this later on to not load an instance.. we have 2 options
                                //1- Make one instance, and use it whenever we need it.. it's always there
                                //2- Don't make an instance, and instead make an instance whenever we use it, then close it
                                //For now we'll just make an instance of all the plugins
                                newPlugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));

                                //Call the initialization sub of the plugin
                                //newPlugin.Instance.Initialize();

                                //Add the new plugin to our collection here
                                //availablePlugins.Add(newPlugin);

                                //cleanup a bit
                                newPlugin = null;
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
        }
    }
}
