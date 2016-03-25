using System;

namespace GameStateTest
{
    public class GamePlayGameState : IGameState
    {
        public void Entered()
        {
            Console.WriteLine("Game: Entering");
        }

        public void Exiting()
        {
            Console.WriteLine("Game: Exiting");
        }

        public void Obscuring()
        {
            Console.WriteLine("Game: Obscuring");
        }

        public void Revealed()
        {
            Console.WriteLine("Game: Revealed");
        }
    }
}