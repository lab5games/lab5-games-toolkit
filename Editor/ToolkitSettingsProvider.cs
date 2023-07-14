using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lab5Games.Editor
{
    // https://qiita.com/sune2/items/a88cdee6e9a86652137c
    // https://www.bzetu.com/413/.html
    // https://docs.unity3d.com/cn/2019.4/ScriptReference/SettingsProvider.html

    public class ToolkitSettingsProvider : SettingsProvider
    {
        ToolkitSettings _settings = null;

        [SettingsProvider]
        static SettingsProvider CreateProvider()
        {
            return new ToolkitSettingsProvider("Project/Lab5 Games Toolkit", SettingsScope.Project, null);
        }

        public ToolkitSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
            _settings = ToolkitSettings.Read();
        }

        public override void OnGUI(string searchContext)
        {
            // settings
            EditorGUILayout.Space(15);
            
            EditorGUI.BeginChangeCheck();

            _settings.AutoBuildVersion = EditorGUILayout.Toggle("Auto Build Version", _settings.AutoBuildVersion);
            _settings.VersionControl = (VersionControlMethods)EditorGUILayout.EnumPopup("Version Control Method", _settings.VersionControl);

            if(EditorGUI.EndChangeCheck())
            {
                ToolkitSettings.Save(_settings);
            }


            // buttons
            EditorGUILayout.Space(6);
            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("Organize Project Folders", GUILayout.Width(240))) ProjectOrganization.CreateProjectFolders();

            EditorGUILayout.EndVertical();
        }
    }
}