
namespace Lab5Games
{
    public delegate void LogDelegate(string message, string filter, UnityEngine.Object context);
    
    public static class GameLogger 
    {
        public enum LogFilter
        {
            Log,
            Warning,
            Error,
            Exception,
            Assert,

            System,
            Network,
            Game
        }

        public static LogDelegate onLog;

        static GameLogger()
        {
            onLog = new LogDelegate(PrintToConsole);
        }

        public static void Log(object obj, LogFilter filter, UnityEngine.Object context = null)
        {
            Log(obj.ToString(), filter, context);
        }

        public static void Log(string msg, LogFilter filter, UnityEngine.Object context = null)
        {
            Log(msg, filter.ToString(), context);
        }

        public static void Log(string msg, string filter, UnityEngine.Object context = null)
        {
            onLog(msg, filter, context);
        }

        public static void PrintToConsole(string msg, string filter, UnityEngine.Object context)
        {
            UnityEngine.Debug.Log(msg + "\nCPAPI:{\"cmd\":\"Filter\", \"name\":\"" + filter + "\"}", context);
        }

        public static void Watch(string displayName, string watchValue)
        {
            UnityEngine.Debug.Log(displayName + " : " + watchValue + "\nCPAPI:{\"cmd\":\"Watch\", \"name\":\"" + displayName + "\"}");
        }
    }
}
