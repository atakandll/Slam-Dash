using System;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        private bool _isAvailableForInput;

        private void OnEnable() => SubscribeEvents();
        
        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
            PlayerSignals.Instance.onPlayerMove += OnPlayerMove;
        }

        private void OnMoveConditionChanged(bool state) => _isAvailableForInput = state;
        private void OnPlayerMove(Vector2Int direction)
        {
                int count = MoveOnceRecursive(direction);
                if (count > 0)
                {
                    //lose health
                    //particle
                    Debug.Log(count);
                }
                
      
        }

        private int MoveOnceRecursive(Vector2Int direction, int moveCount = 0)
        {
            if (moveCount > 1000)
            {
                Debug.Log("Too many moves");
                return moveCount;
            }

            //if (_isAvailableForInput)
            
                var nextPosition = transform.position + new Vector3(direction.x, direction.y, 0);
         
                if (playerManager.CheckForWallForMovement(nextPosition))
                {
                    transform.position = nextPosition;

                    return MoveOnceRecursive(direction, moveCount + 1); // Recursive call
            
                }
            
            
            return moveCount;
        }
        private void OnDisable() => UnsubscribeEvents();
        
        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
            PlayerSignals.Instance.onPlayerMove -= OnPlayerMove;
        }
    }
}