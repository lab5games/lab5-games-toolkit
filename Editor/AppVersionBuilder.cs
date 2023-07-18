using UnityEngine;
using UnityEditor;
using System;

namespace Lab5Games.Editor
{
    public static class AppVersionBuilder
    {
        
        public static AppVersion GetCurrentVersion()
        {
            string bundleVer = PlayerSettings.bundleVersion;
            string[] arrVer = bundleVer.Split(',');

            if (arrVer.Length != 3)
                arrVer = new string[] { "0", "0", "0" };

            AppVersion appVer;
            appVer.major = Mathf.Max(0, Convert.ToInt32(arrVer[0]));
            appVer.minor = Mathf.Max(0, Convert.ToInt32(arrVer[1]));
            appVer.build = Mathf.Max(0, Convert.ToInt32(arrVer[2]));

            return appVer;
        }

        public static AppVersion BuildNewVersion()
        {
            AppVersion appVer = GetCurrentVersion();
            appVer.build++;

            SetNewVersion(appVer);

            return appVer;
        }

        static void SetNewVersion(AppVersion appVer)
        {
            PlayerSettings.bundleVersion = appVer.ToString();
            PlayerSettings.iOS.buildNumber = appVer.build.ToString();
            PlayerSettings.Android.bundleVersionCode = appVer.build;

            Debug.Log($"[AppVersionBuilder] Set new version({appVer})");
        }
    }
}
