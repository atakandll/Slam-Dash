using Prototype.Scripts.Runtime.Enums;
using Prototype.Scripts.Runtime.Interfaces;
using Prototype.Scripts.Runtime.Managers;
using UnityEngine;

namespace Prototype.Scripts.Runtime.Controllers
{
   public class PlayerMovementController : MonoBehaviour
   {
      [SerializeField] private LayerMask environmentLayerMask;
      [SerializeField] private LayerMask interactableLayerMask;
      private PlayerEffectController _playerEffectController;

      private void Awake() => _playerEffectController = GetComponent<PlayerEffectController>();
      public void Move(Vector2Int direction)
      {
         if (GameManager.Instance.GetGameState() == GameState.Playing)
         {
            int count = MoveOnceRecursive(direction);

            if (count > 0)
            {
               _playerEffectController.HitWall(direction);
               GameManager.Instance.TurnTaken();

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
