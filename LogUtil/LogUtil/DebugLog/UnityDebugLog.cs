#if UNITY_ENGINE
using System;
using System.Collections.Generic;
using System.Text;

namespace LogUtil
{
    public class UnityDebugLog : IDebugLog
    {
        private const string ColorFormat = "<color=#{0}{1}{2}{3}>{4}</color>";

        private const string TimeStringFormat = "{0,-25}";
        private const string TagStringFormat = "{1,-15}";
        private const string ContentStringFormat = "{2}";
        private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";
        private const string ToStringFormat = "X2";

        private Dictionary<int, VisiblePack> _tagVisible = new Dictionary<int, VisiblePack>();
        private Dictionary<int, string> _tagColors = new Dictionary<int, string>();

        private StringBuilder _stringBuilder = new StringBuilder();
        private VisiblePack _isLogVisible = VisiblePack.All;
        private bool _isTimeVisible = false;
        private bool _isTagStringVisible = false;

        private string _formatString = string.Empty;

        private Dictionary<string, LogColor> _preTagColors;

        public UnityDebugLog()
        {
            _formatString = TimeStringFormat + TagStringFormat + ContentStringFormat;
        }

        public void SetTimeVisible(bool visible)
        {
            _isTimeVisible = visible;
            GenerateFormatString();
        }

        public void SetLogVisible(VisiblePack visible)
        {
            _isLogVisible = visible;
        }

        public void SetTagStringVisible(bool visible)
        {
            _isTagStringVisible = visible;
            GenerateFormatString();
        }

        public void SetTagVisible(Dictionary<string, VisiblePack> tagVisible)
        {
            _tagVisible.Clear();
            if (tagVisible == null || tagVisible.Count == 0)
            {
                return;
            }

            foreach (KeyValuePair<string, VisiblePack> pair in tagVisible)
            {
                _tagVisible.Add(pair.Key.GetHashCode(), pair.Value);
            }
        }

        public void SetTagColor(Dictionary<string, LogColor> tagColors)
        {
            _preTagColors = tagColors;
            GenerateFormatString();
        }

        public void ShowErrorLog(LogTag tag, object logMessage)
        {
            if (IsVisible(tag, VisiblePack.Severity.Error))
            {
                UnityEngine.Debug.LogError(AddColor(tag, logMessage));
            }
        }

        public void ShowErrorLog(LogTag tag, object logMessage, UnityEngine.Object context)
        {
            if (IsVisible(tag, VisiblePack.Severity.Error))
            {
                UnityEngine.Debug.LogError(AddColor(tag, logMessage), context);
            }
        }

        public void ShowWarningLog(LogTag tag, object logMessage)
        {
            if (IsVisible(tag, VisiblePack.Severity.Warning))
            {
                UnityEngine.Debug.LogWarning(AddColor(tag, logMessage));
            }
        }

        public void ShowWarningLog(LogTag tag, object logMessage, UnityEngine.Object context)
        {
            if (IsVisible(tag, VisiblePack.Severity.Warning))
            {
                UnityEngine.Debug.LogWarning(AddColor(tag, logMessage), context);
            }
        }

        public void ShowLog(LogTag tag, object logMessage)
        {
            if (IsVisible(tag, VisiblePack.Severity.Info))
            {
                UnityEngine.Debug.Log(AddColor(tag, logMessage));
            }
        }

        public void ShowLog(LogTag tag, object logMessage, UnityEngine.Object context)
        {
            if (IsVisible(tag, VisiblePack.Severity.Info))
            {
                UnityEngine.Debug.Log(AddColor(tag, logMessage), context);
            }
        }

        public void ShowErrorLogFormat(LogTag tag, string logMessage, params object[] o)
        {
            if (IsVisible(tag, VisiblePack.Severity.Error))
            {
                UnityEngine.Debug.LogError(AddColorFormat(tag, logMessage, o));
            }
        }

        public void ShowWarningLogFormat(LogTag tag, string logMessage, params object[] o)
        {
            if (IsVisible(tag, VisiblePack.Severity.Warning))
            {
                UnityEngine.Debug.LogWarning(AddColorFormat(tag, logMessage, o));
            }
        }

        public void ShowLogFormat(LogTag tag, string logMessage, params object[] o)
        {
            if (IsVisible(tag, VisiblePack.Severity.Info))
            {
                UnityEngine.Debug.Log(AddColorFormat(tag, logMessage, o));
            }
        }

        private bool IsVisible(LogTag tag, VisiblePack.Severity severity)
        {
            if (!_isLogVisible.GetVisibility(severity))
            {
                return false;
            }

            if (_tagVisible == null)
            {
                return true;
            }

            VisiblePack visible;
            return !_tagVisible.TryGetValue(tag.GetHashCode(), out visible) || visible.GetVisibility(severity);
        }

        private string AddColor(LogTag tag, object msg)
        {
            _stringBuilder.Remove(0, _stringBuilder.Length);

            if (_tagColors.Count == 0)
            {
                return _stringBuilder.AppendFormat(_formatString,
                    DateTime.Now.ToString(TimeFormat), tag, msg).ToString();
            }


            string colorFormat;
            if (!_tagColors.TryGetValue(tag.GetHashCode(), out colorFormat))
            {
                return _stringBuilder.AppendFormat(_formatString,
                    DateTime.Now.ToString(TimeFormat), tag, msg).ToString();
            }

            return _stringBuilder.AppendFormat(colorFormat,
                DateTime.Now.ToString(TimeFormat), tag, msg).ToString();
        }

        private string AddColorFormat(LogTag tag, string msg, params object[] o)
        {
            _stringBuilder.Remove(0, _stringBuilder.Length);

            if (_tagColors.Count == 0)
            {
                return _stringBuilder.AppendFormat(_formatString,
                    DateTime.Now.ToString(TimeFormat), tag,
                    string.Format(msg, o)).ToString();
            }

            string colorFormat;
            if (!_tagColors.TryGetValue(tag.GetHashCode(), out colorFormat))
            {
                return _stringBuilder.AppendFormat(_formatString,
                    DateTime.Now.ToString(TimeFormat), tag,
                    string.Format(msg, o)).ToString();
            }

            return _stringBuilder.AppendFormat(colorFormat,
                DateTime.Now.ToString(TimeFormat), tag,
                string.Format(msg, o)).ToString();
        }

        private void GenerateFormatString()
        {
            _formatString = string.Empty;
            if (_isTimeVisible)
            {
                _formatString += TimeStringFormat;
            }
            if (_isTagStringVisible)
            {
                _formatString += TagStringFormat;
            }
            _formatString += ContentStringFormat;

            if (_tagColors == null)
            {
                return;
            }

            if (_preTagColors == null)
            {
                return;
            }

            _tagColors.Clear();
            foreach (KeyValuePair<string, LogColor> tagColor in _preTagColors)
            {
                _tagColors.Add(tagColor.Key.GetHashCode(),
                    string.Format(ColorFormat,
                        tagColor.Value.R.ToString(ToStringFormat), tagColor.Value.G.ToString(ToStringFormat),
                        tagColor.Value.B.ToString(ToStringFormat), tagColor.Value.A.ToString(ToStringFormat), _formatString));
            }
        }
    }
}
#endif