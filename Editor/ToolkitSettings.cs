using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace Lab5Games.Editor
{
    public enum VersionControlMethods
    {
        None,
        Svn,
        Git
    }

    public class ToolkitSettings
    {
        public bool AutoBuildVersion;
        public VersionControlMethods VersionControl;

        public static string AssetPath => Path.Combine(Environment.CurrentDirectory, "ProjectSettings/settings.LAB5");

        public ToolkitSettings()
        {
            AutoBuildVersion = true;
            VersionControl = VersionControlMethods.None;
        }


        public static ToolkitSettings CreateNew()
        {
            ToolkitSettings settins =  new ToolkitSettings();

            Debug.Log("[ToolkitSettings] Created");

            return settins;
        }

        public static ToolkitSettings Read()
        {
            if(File.Exists(AssetPath))
            {
                using(FileStream stream = new FileStream(AssetPath, FileMode.Open, FileAccess.Read))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<ToolkitSettings>(reader.ReadToEnd());
                    }
                }
            }
            else
            {
                return CreateNew();
            }
        }

        public static void Save(ToolkitSettings settings)
        {
            if (settings == null)
                throw new System.ArgumentNullException();

            using(FileStream stream = new FileStream(AssetPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    var json = JsonConvert.SerializeObject(settings);
                    writer.Write(json);

                    Debug.Log("[ToolkitSettings] Saved");
                }
            }
        }
    }
}