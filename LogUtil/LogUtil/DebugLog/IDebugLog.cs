using System.Collections.Generic;

namespace LogUtil
{
    public class LogColor
    {
        public short R { get; set; }
        public short G { get; set; }
        public short B { get; set; }
        public short A { get; set; }
    }

    [System.Serializable]
    public class VisiblePack
    {
        public enum Severity
        {
            Info,
            Warning,
            Error,
            Exception
        }

        public bool Info;
        public bool Warning;
        public bool Error;
        public bool Exception;

        public VisiblePack(bool info = true, bool warning = true, bool error = true, bool exception = true)
        {
            Info = info;
            Warning = warning;
            Error = error;
            Exception = exception;
        }

        public static VisiblePack All
        {
            get { return new VisiblePack() { Info = true, Warning = true, Error = true, Exception = true }; }
        }

        public static VisiblePack None
        {
            get { return new VisiblePack() { Info = false, Warning = false, Error = false, Exception = false }; }
        }

        public bool GetVisibility(Severity severity)
        {
            switch (severity)
            {
                case Severity.Info:
                    return Info;
                case Severity.Warning:
                    return Warning;
                case Severity.Error:
                    return Error;
                case Severity.Exception:
                    return Exception;
                default:
                    return false;
            }
        }
    }

    public interface IDebugLog
    {
        void SetLogVisible(VisiblePack visible);
        void SetTimeVisible(bool visible);
        void SetTagStringVisible(bool visible);
        void SetTagVisible(Dictionary<string, VisiblePack> tagVisible);
        void SetTagColor(Dictionary<string, LogColor> tagColor);

        void ShowErrorLog(LogTag tag, object logMessage,
            string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0);

        void ShowWarningLog(LogTag tag, object logMessage,
            string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0);

        void ShowLog(LogTag tag, object logMessage,
            string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0);
    }
}