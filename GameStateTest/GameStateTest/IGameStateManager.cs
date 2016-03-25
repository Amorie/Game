namespace GameStateTest
{
    public interface IGameStateManager
    {
        void Switch(IGameState state);
        void Pop();
        void Push(IGameState state);
        IGameState Peek();
    }
}