using Runtime.Interface;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Movement
{
    public class MoveRightCommand : IMoveCommand
    {
        
        public void Execute()
        {
            PlayerSignals.Instance.onPlayerMove?.Invoke(Vector2Int.right);
        }
    }
}