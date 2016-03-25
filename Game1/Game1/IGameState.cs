namespace Game1
{
    public interface IGameState
    {
        void Entered();
        void Leaving();
        void Obscuring();
        void Revealed();
    }
}