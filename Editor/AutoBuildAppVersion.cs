using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;

namespace Lab5Games.Editor
{
    public class AutoBuildAppVersion : IPostprocessBuildWithReport
    {
        public int callbackOrder { get; }


        public void OnPostprocessBuild(BuildReport report)
        {
            ToolkitSettings settings = ToolkitSettings.Read();

            if (settings.AutoBuildVersion == false)
                return;

            ReplaceNewVersion();
            ExecuteVersionControl(settings.VersionControl);

            AssetDatabase.SaveAssets();
        }


        void ReplaceNewVersion()
        {
            string strVer = PlayerSettings.bundleVersion;
            string[] arrVer = strVer.Split('.');

            if (arrVer.Length != 3)
                arrVer = new string[] {"0", "0", "0"};

            int major = Mathf.Max(0, Convert.ToInt32(arrVer[0]));
            int minor = Mathf.Max(0, Convert.ToInt32(arrVer[1]));
            int build = Mathf.Max(0, Convert.ToInt32(arrVer[2])) + 1;

            string newVer = string.Format("{0}.{1}.{2}", major, minor, build);
            
            PlayerSettings.bundleVersion = newVer;
            PlayerSettings.iOS.buildNumber = build.ToString();
            PlayerSettings.Android.bundleVersionCode = build;
            
            UnityEngine.Debug.Log($"[AutoBuildAppVersion] New version({newVer}) build");
        }

        void ExecuteVersionControl(VersionControlMethods method)
        {
            switch(method)
            {
                case VersionControlMethods.None:
                    UnityEngine.Debug.LogWarning("[AutoBuildAppVersion] No version control");
                    break;

                case VersionControlMethods.Svn:
                    SvnVersionControl();
                    break;

                case VersionControlMethods.Git:
                    GitVersionControl();
                    break;
            }
        }

        void SvnVersionControl()
        {
            // update unity ProjectSettings folder
            UnityEngine.Debug.Log("[AutoBuildAppVersion] Svn update...");

            Process.Start(
                "TortoiseProc.exe",
                "/command:update /path:" +
                Path.Combine(Environment.CurrentDirectory, "ProjectSettings") +
                " /closeonend:0");

            // commit untiy ProjectSettings folder
            UnityEngine.Debug.Log("[AutoBuildAppVersion] Svn commit...");

            Process.Start(
                "TortoiseProc.exe",
                "/command:commit /path:" +
                Path.Combine(Environment.CurrentDirectory, "ProjectSettings") +
                " /closeonend:0");
        }

        void GitVersionControl()
        {
            UnityEngine.Debug.LogWarning("[AutoBuildAppVersion] Git version control is not yet available");
        }
    }
}