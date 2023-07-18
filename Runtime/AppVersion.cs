
namespace Lab5Games
{
    public struct AppVersion 
    {
        public int major;
        public int minor;
        public int build;

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", major, minor, build);
        }
    }
}
