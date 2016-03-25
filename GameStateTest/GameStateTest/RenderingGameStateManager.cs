using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStateTest
{
    public class RenderingGameStateManager : IGameStateManager
    {
        private readonly Stack<IGameState> _currentStates;

        public RenderingGameStateManager()
        {
            _currentStates = new Stack<IGameState>();
        }
        //change the state
        public void Switch(IGameState state)
        {
            //what am i going to do here
            
        }

        //remove top gamestate of stack
        public void Pop()
        {
            if(_currentStates.Count != 0)
            _currentStates.Peek().Exiting();
            _currentStates.Pop();
            if (_currentStates.Count != 0)
            {
                _currentStates.Peek().Revealed();
            }
        }

        //put a new gamestate on stack
        public void Push(IGameState state)
        {
            if (_currentStates.Count != 0)
            {
                _currentStates.Peek().Obscuring();
            }
            state.Entered();
            _currentStates.Push(state);
            
        }
        // look at the top of the stack 
        public IGameState Peek()
        {
            return _currentStates.Peek();
        }
    }
}