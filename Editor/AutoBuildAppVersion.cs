using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Lab5Games.Editor
{
    public class AutoBuildAppVersion : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }

        ToolkitSettings _toolkitSettins = null;
        ToolkitSettings toolkitSettings
        {
            get
            {
                if (_toolkitSettins == null)
                    _toolkitSettins = ToolkitSettings.Read();

                return _toolkitSettins;
            }
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (toolkitSettings.AutoBuildVersion)
            {
                VersionControl();
            }
        }

        private void VersionControl()
        {
            switch(toolkitSettings.VersionControl)
            {
                case VersionControlMethods.Svn:
                    SvnControl.SafeCommit(GetControlPaths());
                    break;

                case VersionControlMethods.Git:
                    UnityEngine.Debug.LogWarning("[AutoBuildAppVersion] Git is not yet available!");
                    break;
            }
        }

        private string GetControlPaths()
        {
            string controlPaths = Path.Combine(Environment.CurrentDirectory, "ProjectSettings");

            return controlPaths;
        }
    }
}