using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LogUtil
{
    public class Debug
    {
        private static IDebugLog _debugLog;
        private static string _pathRoot;
#if UNITY_ENGINE
        private static UnityDebugLog _debugLogUnity;
#endif

        static Debug()
        {
#if UNITY_ENGINE
            _debugLogUnity = new UnityDebugLog();
            _debugLog = _debugLogUnity;
#else
            _debugLog = new DefaultErrorLog();
#endif
        }

        /// <summary>
        /// 設定log行為的執行者
        /// </summary>
        /// <param name="log"></param>
        public static void SetDebugLogInterface(IDebugLog log)
        {
            if (log == null)
            {
#if UNITY_ENGINE
                _debugLogUnity = new UnityDebugLog();
                _debugLog = _debugLogUnity;
#else
                _debugLog = new DefaultErrorLog();
#endif
            }

            lock (_debugLog)
            {
                _debugLog = log;
#if UNITY_ENGINE
                _debugLogUnity = log as UnityDebugLog;
#endif
            }
        }

        /// <summary>
        /// 是否顯示log
        /// </summary>
        /// <param name="visible"></param>
        public static void SetAllLogVisible(VisiblePack visible)
        {
            lock (_debugLog)
            {
                _debugLog.SetLogVisible(visible);
            }
        }

        /// <summary>
        /// 設定tag是否顯示
        /// </summary>
        /// <param name="tagVisible"></param>
        public static void SetTagVisible(Dictionary<string, VisiblePack> tagVisible)
        {
            lock (_debugLog)
            {
                _debugLog.SetTagVisible(tagVisible);
            }
        }


        /// <summary>
        /// 設定tag的顏色
        /// </summary>
        /// <param name="tagColor"></param>
        public static void SetTagColor(Dictionary<string, LogColor> tagColor)
        {
            lock (_debugLog)
            {
                _debugLog.SetTagColor(tagColor);
            }
        }

        /// <summary>
        /// 設定是否顯示時間
        /// </summary>
        /// <param name="visible"></param>
        public static void SetTimeVisible(bool visible)
        {
            lock (_debugLog)
            {
                _debugLog.SetTimeVisible(visible);
            }
        }

        /// <summary>
        /// 設定是否顯示tag字串
        /// </summary>
        /// <param name="visible"></param>
        public static void SetTagStringVisible(bool visible)
        {
            lock (_debugLog)
            {
                _debugLog.SetTagStringVisible(visible);
            }
        }

        public static void SetRelativePathRoot(string pathRoot)
        {
            _pathRoot = pathRoot;
            if (_pathRoot[_pathRoot.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                _pathRoot += System.IO.Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tag"></param>
        public static void LogError(object msg,
            LogTag tag = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            lock (_debugLog)
            {
                if (tag == null)
                {
                    tag = LogTag.Default;
                }
                
                if(!string.IsNullOrEmpty(_pathRoot))
                {
                    var index = sourceFilePath.IndexOf(_pathRoot);
                    if(index >= 0)
                    {
                        sourceFilePath = sourceFilePath.Substring(index + _pathRoot.Length);
                    }
                }
                _debugLog.ShowErrorLog(tag, msg, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tag"></param>
        public static void LogWarning(object msg,
            LogTag tag = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            lock (_debugLog)
            {
                if (tag == null)
                {
                    tag = LogTag.Default;
                }

                if (!string.IsNullOrEmpty(_pathRoot))
                {
                    var index = sourceFilePath.IndexOf(_pathRoot);
                    if (index >= 0)
                    {
                        sourceFilePath = sourceFilePath.Substring(index + _pathRoot.Length);
                    }
                }
                _debugLog.ShowWarningLog(tag, msg, memberName, sourceFilePath, sourceLineNumber);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tag"></param>
        public static void Log(object msg,
            LogTag tag = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            lock (_debugLog)
            {
                if (tag == null)
                {
                    tag = LogTag.Default;
                }

                if (!string.IsNullOrEmpty(_pathRoot))
                {
                    var index = sourceFilePath.IndexOf(_pathRoot);
                    if (index >= 0)
                    {
                        sourceFilePath = sourceFilePath.Substring(index + _pathRoot.Length);
                    }
                }
                _debugLog.ShowLog(tag, msg, memberName, sourceFilePath, sourceLineNumber);
            }
        }

#if UNITY_ENGINE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tag"></param>
        public static void LogError(object msg, UnityEngine.Object context, LogTag tag = null)
        {
            lock (_debugLog)
            {
                if (tag == null)
                {
                    tag = LogTag.Default;
                }

                if (_debugLogUnity != null)
                {
                    _debugLogUnity.ShowErrorLog(tag, msg, context);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tag"></param>
        public static void LogWarning(object msg, UnityEngine.Object context, LogTag tag = null)
        {
            lock (_debugLog)
            {
                if (tag == null)
                {
                    tag = LogTag.Default;
                }

                if (_debugLogUnity != null)
                {
                    _debugLogUnity.ShowWarningLog(tag, msg, context);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tag"></param>
        public static void Log(object msg, UnityEngine.Object context, LogTag tag = null)
        {
            lock (_debugLog)
            {
                if (tag == null)
                {
                    tag = LogTag.Default;
                }

                if (_debugLogUnity != null)
                {
                    _debugLogUnity.ShowLog(tag, msg, context);
                }
            }
        }
#endif
    }
}