using UnityEditor;
using System;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Lab5Games.Editor
{
    // https://blog.csdn.net/weixin_44389880/article/details/113699329
    // https://www.cnblogs.com/andrew-blog/archive/2012/08/21/SVN_DOS_Commands.html
    // https://itmonon.blog.csdn.net/article/details/127475196?spm=1001.2101.3001.6650.1&utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1-127475196-blog-113699329.235%5Ev38%5Epc_relevant_sort_base2&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-1-127475196-blog-113699329.235%5Ev38%5Epc_relevant_sort_base2&utm_relevant_index=2&ydreferer=aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80NDM4OTg4MC9hcnRpY2xlL2RldGFpbHMvMTEzNjk5MzI5

    public class SvnControl 
    {
        [MenuItem("Assets/Svn Control/Update", priority = 0)]
        public static void Update()
        {
            string controlPaths = Path.Combine(Environment.CurrentDirectory, "ProjectSettings/ProjectSettings.asset");
            controlPaths = controlPaths + "*" + GetClickedDirFullPath();

            Update(controlPaths);
        }

        [MenuItem("Assets/Svn Control/Commit", priority = 1)]
        public static void Commit()
        {
            string controlPaths = Path.Combine(Environment.CurrentDirectory, "ProjectSettings/ProjectSettings.asset");
            controlPaths = controlPaths + "*" + GetClickedDirFullPath();

            Commit(controlPaths);
        }

        [MenuItem("Assets/Svn Control/Safe Commit", priority = 2)]
        public static void SafeCommit()
        {
            Update();
            Commit();
        }

        public static void Update(string path)
        {
            Process.Start(
                "TortoiseProc.exe",
                "/command:update /path:\"" + path + "\"" +
                " /closeonend:1");

            AssetDatabase.Refresh();

            Debug.LogWarning("[SvnControl] Finished update");
        }

        public static void Commit(string path)
        {
            AppVersion appVer = AppVersionBuilder.BuildNewVersion();

            Process.Start(
                "TortoiseProc.exe",
                "/command:commit /path:\"" + path + "\"" +
                " /logmsg \"auto svn commit: " + appVer + "\"" +
                " /closeonend:1");

            Debug.LogWarning("[SvnControl] Finished Commit");
        }

        public static void SafeCommit(string path)
        {
            Update(path);
            Commit(path);
        }

        private static string GetClickedDirFullPath()
        {
            string clickedAssetGuid = Selection.assetGUIDs[0];
            string clickedPath = AssetDatabase.GUIDToAssetPath(clickedAssetGuid);
            string clickedPathFull = Path.Combine(Directory.GetCurrentDirectory(), clickedPath);

            FileAttributes attr = File.GetAttributes(clickedPathFull);
            return attr.HasFlag(FileAttributes.Directory) ? clickedPathFull : Path.GetDirectoryName(clickedPathFull);
        }
    }
}
