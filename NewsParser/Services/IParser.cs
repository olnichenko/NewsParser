namespace NewsParser.Services
{
    public interface IParser
    {
        bool LoadDocument();
        void Parse();
    }
}
