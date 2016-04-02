namespace Game1
{
    public interface IGameState
    {

        void Entered(); //called when put in the manager
        void Exiting();// called before being put in the manager
        void Obscuring(); //called before another state is pushed over top of this one.
        void Revealed(); // called when becomes the top of the stack.
    }
}