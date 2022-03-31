using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace LogUtil
{
    public class DefaultErrorLog : IDebugLog
    {
        private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string Space = " ";

        private Dictionary<LogTag, string> _tagDic = new Dictionary<LogTag, string>();

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

        public void ShowErrorLog(LogTag tag, object log,
            string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[E] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(DateTime.Now.ToString(TimeFormat));
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(GetTagString(tag));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{Path.GetFileName(sourceFilePath)}, func={memberName}, line={sourceLineNumber} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(log.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowWarningLog(LogTag tag, object log,
            string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[W] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(DateTime.Now.ToString(TimeFormat));
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(GetTagString(tag));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{Path.GetFileName(sourceFilePath)}, func={memberName}, line={sourceLineNumber} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(log.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ShowLog(LogTag tag, object log,
            string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[I] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(DateTime.Now.ToString(TimeFormat));
            Console.Write(Space);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(GetTagString(tag));
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{Path.GetFileName(sourceFilePath)}, func={memberName}, line={sourceLineNumber} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(log.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private string GetTagString(LogTag tag)
        {
            if(!_tagDic.TryGetValue(tag, out var tagStr))
            {
                tagStr = string.Format("{0,-10}", tag.ToString());
                _tagDic.Add(tag, tagStr);
            }
            return tagStr;
        }
    }
}