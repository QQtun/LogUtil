using SkyGame.LogTagType;

namespace SkyGame
{
    public abstract class LogTag
    {
        public static readonly LogTag Default = new Default();
        public static readonly LogTag Designer = new Designer();
        public static readonly LogTag System = new LogTagType.System();
        public static readonly LogTag Network = new Network();
        public static readonly LogTag EventSystem = new EventSystem();

        protected string _tag;
        private int _hashCode;

        protected LogTag(string tag)
        {
            _tag = tag;
            _hashCode = _tag.GetHashCode();
        }

        public override string ToString()
        {
            return _tag;
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }

    namespace LogTagType
    {
        internal class Default : LogTag
        {
            public Default()
                : base(typeof(Default).Name)
            {
            }
        }

        internal class Designer : LogTag
        {
            public Designer()
                : base(typeof(Designer).Name)
            {
            }
        }

        internal class System : LogTag
        {
            public System()
                : base(typeof(System).Name)
            {
            }
        }

        internal class Network : LogTag
        {
            public Network()
                : base(typeof(Network).Name)
            {
            }
        }

        internal class EventSystem : LogTag
        {
            public EventSystem()
                : base(typeof(EventSystem).Name)
            {
            }
        }
    }
}