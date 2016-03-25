using System;

namespace GameStateTest
{
    public class MenuGameState : IGameState
    {
        

        public void Entered()
        {
            Console.WriteLine("Menu: Entered the manager");
        }

        public void Exiting()
        {
            Console.WriteLine("Menu: Exiting the manger");
        }

        public void Obscuring()
        {
            Console.WriteLine("Menu: Being obscured by another state");
        }

        public void Revealed()
        {
            Console.WriteLine("Menu: Now the top of the stack");
        }
    }
}