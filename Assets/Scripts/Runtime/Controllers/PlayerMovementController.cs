using Runtime.Enums;
using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers
{
   public class PlayerMovementController : MonoBehaviour
   {
      [SerializeField] private LayerMask environmentLayerMask;
      [SerializeField] private LayerMask interactableLayerMask;
      private PlayerEffectController _playerEffectController;
      private GameManager _gameManager;

      private void Awake()
      {
         _gameManager = FindObjectOfType<GameManager>();
         _playerEffectController = GetComponent<PlayerEffectController>();
      }

      public void Move(Vector2Int direction)
      {
         if (_gameManager.GetGameState() == GameState.Playing)
         {
            int count = MoveOnceRecursive(direction);

            if (count > 0)
            {
               _playerEffectController.HitWall(direction);
               _gameManager.TurnTaken();

            }

         }
      
      }
      private int MoveOnceRecursive(Vector2Int direction, int moveCount = 0)
      {
         //bail
         if (moveCount > 1000)
         {
            Debug.Log("Too many moves");
            return moveCount;
         }
      
         Vector3 nextPosition = transform.position + new Vector3(direction.x, direction.y, 0);
         
         if (IsNoWallHere(nextPosition))
         {
            //spawn dust trail
            _playerEffectController.DustTrail(transform.position);
            transform.position = nextPosition;
            CheckForInteractable(transform.position);

            return MoveOnceRecursive(direction, moveCount + 1); // Recursive call
            
         }

         return moveCount;
      }

      private void CheckForInteractable(Vector3 worldPoint)
      {
         Collider2D overlap = Physics2D.OverlapPoint(worldPoint, interactableLayerMask);

         if (overlap != null)
         {
            // Interact with pickup
            var interactable = overlap.GetComponent<IInteractable>();
            interactable?.Interact();
         }
      }

      private bool IsNoWallHere(Vector3 worldPoint)
      {
         Collider2D wall = Physics2D.OverlapPoint(worldPoint, environmentLayerMask);
         return wall == null;
      }
   }
}
