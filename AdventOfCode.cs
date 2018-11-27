namespace AdventOfCode
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

	public class AdventOfCode
	{
		public static void Main(string[] args)
		{
            ScriptSettings settings = null;
            bool scriptSuccess      = true;
            Stopwatch stopWatch     = new Stopwatch();
            string scriptFolderName = ConfigurationManager.AppSettings["ScriptStartupFolder"];
            string settingsPath     = Path.Combine(scriptFolderName, "ScriptSettings.xml");

            try
            {
                settings = LoadFileIntoObject(settingsPath);

                if (settings == null)
                    throw new InvalidOperationException($"Script settings is empty for path: {settingsPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while loading Script Settings. \n Exception \n {ex.ToString()}");
                Exit();
            }

            string startupClass     = $"AdventOfCode.AoC{scriptFolderName}.Day{settings.Day}";
            AdventOfCodeBase script = null;

            try
            {
                Type scriptType = Assembly.GetExecutingAssembly().GetType(startupClass);
                if (scriptType == null)
                    throw new InvalidOperationException($"Unable to get type for the startup class {startupClass}");

                script = Activator.CreateInstance(scriptType) as AdventOfCodeBase;
                if (script == null)
                    throw new InvalidOperationException($"Failed to create instance of: {scriptType.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to create an instance of the script. \n Exception \n {ex.ToString()}");
            }

            try
            {
                stopWatch.Start();

                Console.WriteLine("Running Script...");
                script.Main();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured whilst processing the script. \n Exception \n {ex.ToString()}");
                scriptSuccess = false;
            }

            stopWatch.Stop();

            Console.WriteLine($"\nScript {(scriptSuccess ? "completed successfully" : "failed")}. Run duration: {stopWatch.Elapsed.ToString()} seconds.");

            Exit();
        }

        /// <summary>Loads the file into object.</summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A filled class from the xml</returns>
        /// <exception cref="InvalidOperationException">Unable to load ScriptSetting file into memory. \n Exception \n {ex.ToString()}</exception>
        private static ScriptSettings LoadFileIntoObject(string fileName)
        {
            try
            {
                // Override the encoding method to UTF8 to prevent certain characters breaking the XML reader
                using (StreamReader streamReader = new StreamReader(fileName, Encoding.UTF8))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScriptSettings));
                    return (ScriptSettings)xmlSerializer.Deserialize(streamReader);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to load ScriptSetting file into memory. \n Exception \n {ex.ToString()}");
            }
        }

        /// <summary>Exits this instance.</summary>
        private static void Exit()
        {
            Console.WriteLine("Press any key to end.");
            Console.ReadKey(true);

            Environment.Exit(1);
        }

        [XmlRoot("ScriptSettings")]
        public class ScriptSettings
        {
            [XmlElement("Day")]
            public string Day { get; set; }
        }
	}
}
