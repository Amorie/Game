using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateTest
{
    class Program
    {

 
        static void Main(string[] args)
        {
            var renderer = new RenderingGameStateManager();
            var menu = new MenuGameState();
            var game = new GamePlayGameState();

            renderer.Push(menu);
            renderer.Push(game);
            renderer.Pop();
            renderer.Pop();
            Console.ReadKey();

        }
    }
}
