namespace Interfaces
{
    public interface Initializable
    {
        bool Initialized { get; }
        void Initialize();
    }
}