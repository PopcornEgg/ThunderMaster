using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugins
{
    public class Debug
    {
        static object locked = new object();
        public static System.Action<string> OnLog = null;
        public static void OnException(Exception e)
        {
            LogWarning(e.Message);
            LogWarning(e.Source);
            LogWarning(e.StackTrace);
        }

        public static void LogLine(string msg)
        {
            if (OnLog != null) OnLog(msg);
            else Console.WriteLine(msg);

            LogFile(msg);
        }

        public static void LogLive(string msg)
        {
            if (OnLog != null) OnLog("LIVE#"+msg);
            else Console.Write(msg);
            
            LogFile(msg);
        }

        public static void LogError(string msg)
        {
            if (OnLog != null) OnLog("ERROR#"+msg);
            else Console.Write(msg);

            LogFile(msg);
        }

        public static void LogWarning(string msg)
        {
            if (OnLog != null) OnLog("WARNING#" + msg);
            else Console.Write(msg);

            LogFile(msg);
        }

        static void LogFile(string msg)
        {
            lock (locked)
            {
                System.IO.File.AppendAllText("log/" + DateTime.Now.ToString("yyyy年M月d日") + ".log", DateTime.Now.ToString() + "\t" + msg + "\r\n");
            }
        }
    }

    public class Time
    {
        DateTime end;
        public Time() { }
        public Time(TimeSpan t) { end = DateTime.Now + t; }
        public Time(int hours, int minutes, int seconds) { end = DateTime.Now + new TimeSpan(hours, minutes, seconds); }
        public void Start(TimeSpan t) { end = DateTime.Now + t; }
        public void Start(int hours, int minutes, int seconds) { end = DateTime.Now + new TimeSpan(hours, minutes, seconds); }
        public bool End() { return DateTime.Now > end; }
    }
}
