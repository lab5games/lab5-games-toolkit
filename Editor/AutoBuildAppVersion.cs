using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;

namespace Lab5Games.Editor
{
    // https://blog.csdn.net/weixin_44389880/article/details/113699329
    // https://www.cnblogs.com/andrew-blog/archive/2012/08/21/SVN_DOS_Commands.html
    // https://itmonon.blog.csdn.net/article/details/127475196?spm=1001.2101.3001.6650.1&utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1-127475196-blog-113699329.235%5Ev38%5Epc_relevant_sort_base2&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1-127475196-blog-113699329.235%5Ev38%5Epc_relevant_sort_base2&utm_relevant_index=2&ydreferer=aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80NDM4OTg4MC9hcnRpY2xlL2RldGFpbHMvMTEzNjk5MzI5

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
            string controlPaths =
                ToolkitSettings.AssetPath + "*" +
                Path.Combine(Environment.CurrentDirectory, "ProjectSettings/ProjectSettings.asset");

            // update unity ProjectSettings folder
            UnityEngine.Debug.Log("[AutoBuildAppVersion] Svn update...");

            Process.Start(
                "TortoiseProc.exe",
                "/command:update /path:\"" + controlPaths + "\"" + 
                " /closeonend:1");
                

            // commit untiy ProjectSettings folder
            UnityEngine.Debug.Log("[AutoBuildAppVersion] Svn commit...");

            Process.Start(
                "TortoiseProc.exe",
                "/command:commit /path:\"" + controlPaths + "\"" +
                $" /logmsg \"" + $"auto version {PlayerSettings.bundleVersion}" + "\"" +  
                " /closeonend:1");
        }

        void GitVersionControl()
        {
            UnityEngine.Debug.LogWarning("[AutoBuildAppVersion] Git version control is not yet available");
        }
    }
}