using Runtime.Interface;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Movement
{
    public class MoveDownCommand : IMoveCommand
    {
        public void Execute()
        {
            PlayerSignals.Instance.onPlayerMove?.Invoke(Vector2Int.down);
        }
    }
}