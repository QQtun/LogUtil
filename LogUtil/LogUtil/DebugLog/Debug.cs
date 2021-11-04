using System.Collections.Generic;

namespace SkyGame
{
    public class Debug
    {
        private static IDebugLog _debugLog;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tag"></param>
        public static void LogError(object msg, LogTag tag = null)
        {
            lock (_debugLog)
            {
                if (tag == null)
                {
                    tag = LogTag.Default;
                }

                _debugLog.ShowErrorLog(tag, msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tag"></param>
        public static void LogWarning(object msg, LogTag tag = null)
        {
            lock (_debugLog)
            {
                if (tag == null)
                {
                    tag = LogTag.Default;
                }

                _debugLog.ShowWarningLog(tag, msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tag"></param>
        public static void Log(object msg, LogTag tag = null)
        {
            lock (_debugLog)
            {
                if (tag == null)
                {
                    tag = LogTag.Default;
                }

                _debugLog.ShowLog(tag, msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="o">最後一個參數可以為LogTag</param>
        public static void LogErrorFormat(string msg, params object[] o)
        {
            lock (_debugLog)
            {
                LogTag tag = LogTag.Default;

                if (o != null && o.Length > 0 && o[o.Length - 1] is LogTag)
                {
                    tag = (LogTag)o[o.Length - 1];
                }

                _debugLog.ShowErrorLogFormat(tag, msg, o);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="o">最後一個參數可以為LogTag</param>
        public static void LogWarningFormat(string msg, params object[] o)
        {
            lock (_debugLog)
            {
                LogTag tag = LogTag.Default;

                if (o != null && o.Length > 0 && o[o.Length - 1] is LogTag)
                {
                    tag = (LogTag)o[o.Length - 1];
                }

                _debugLog.ShowWarningLogFormat(tag, msg, o);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="o">最後一個參數可以為LogTag</param>
        public static void LogFormat(string msg, params object[] o)
        {
            lock (_debugLog)
            {
                LogTag tag = LogTag.Default;

                if (o != null && o.Length > 0 && o[o.Length - 1] is LogTag)
                {
                    tag = (LogTag)o[o.Length - 1];
                }

                _debugLog.ShowLogFormat(tag, msg, o);
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