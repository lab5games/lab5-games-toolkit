using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Lab5Games.Editor
{
    public class ProjectOrganization
    {
        static string[] FolderNames = new string[]
        {
            "Assets/{0}/Art/Animations",
            "Assets/{0}/Art/Materials",
            "Assets/{0}/Art/Models",
            "Assets/{0}/Art/Textures",
            "Assets/{0}/Art/UI",
            "Assets/{0}/Art/Fonts",

            "Assets/{0}/Audio/Musics",
            "Assets/{0}/Audio/Sounds",

            "Assets/{0}/Code/Scripts",
            "Assets/{0}/Code/Shaders",

            "Assets/{0}/Content/Prefabs",
            "Assets/{0}/Content/Scenes",
            "Assets/{0}/Content/Scriptables",


            "Assets/{0}/Docs",
            "Assets/{0}/Settings",

            "Assets/Third Party"
        };

        public static void CreateProjectFolders()
        {
            string projectName = new string(Application.productName.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c)).
                ToArray());

            projectName = projectName.Replace('-', '_');
            projectName = "_" + projectName;

            foreach(var folder in FolderNames)
            {
                string path = string.Format(folder, projectName);

                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }

            AssetDatabase.Refresh();
        }
    }
}
