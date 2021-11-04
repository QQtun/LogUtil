using System;
using System.Collections.Generic;

namespace SkyGame
{
    public class DefaultErrorLog : IDebugLog
    {
        private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string Space = " ";

        public void SetTimeVisible(bool visible)
        {
        }

        public void SetLogVisible(VisiblePack visible)
        {
        }

        public void SetTagStringVisible(bool visible)
        {
        }

        public void SetTagVisible(Dictionary<string, VisiblePack> tagVisible)
        {
        }

        public void SetTagColor(Dictionary<string, LogColor> tagColor)
        {
        }

        public void ShowErrorLog(LogTag tag, object log)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.ToString(TimeFormat));
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(tag);
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(log.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowWarningLog(LogTag tag, object log)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.ToString(TimeFormat));
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(tag);
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(log.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowLog(LogTag tag, object log)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.ToString(TimeFormat));
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(tag);
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(log.ToString());
        }

        public void ShowErrorLogFormat(LogTag tag, string log, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.ToString(TimeFormat));
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(tag);
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(log, args);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowWarningLogFormat(LogTag tag, string log, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.ToString(TimeFormat));
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(tag);
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(log, args);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowLogFormat(LogTag tag, string log, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.ToString(TimeFormat));
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(tag);
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(log, args);
        }
    }
}