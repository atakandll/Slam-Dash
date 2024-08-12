using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers
{
   public class PlayerMovementController : MonoBehaviour
   {
      [SerializeField] private LayerMask environmentLayerMask;
      [SerializeField] private LayerMask interactableLayerMask;
      [SerializeField] private GameObject dustTrailPrefab;
      private GameManager gameManager;

      private void Awake()
      {
         gameManager = FindObjectOfType<GameManager>();
      }

      public void Move(Vector2Int direction)
      {
         if (gameManager.GetGameState() == GameState.Playing)
         {
            MoveOnceRecursive(direction);
            gameManager.TurnTaken();
         }
      
      }
      private void MoveOnceRecursive(Vector2Int direction, int tooManyMoves = 0)
      {
         //bail
         if (tooManyMoves > 1000)
         {
            Debug.Log("Too many moves");
            return;
         }
      
         Vector3 nextPosition = transform.position + new Vector3(direction.x, direction.y, 0);
         if (IsNoWallHere(nextPosition))
         {
            //spawn dust trail
            SpawnDustTrail(transform.position);
            transform.position = nextPosition;
            CheckForInteractable(transform.position);

            MoveOnceRecursive(direction, tooManyMoves + 1); // Recursive call
         }
      }

      private void  CheckForInteractable(Vector3 worldPoint)
      {
         Collider2D overlap = Physics2D.OverlapPoint(worldPoint, interactableLayerMask);

         if (overlap != null)
         {
            // Interact with pickup
            InteractableManager interactableManager = overlap.GetComponent<InteractableManager>();

            if (interactableManager != null)
            {
               interactableManager.Interact();
            }
        

         }
      
      

      }

      private void SpawnDustTrail(Vector3 worldPoint)
      {
         Instantiate(dustTrailPrefab, worldPoint, Quaternion.identity);
      
      }

      private bool IsNoWallHere(Vector3 worldPoint)
      {
         Collider2D wall = Physics2D.OverlapPoint(worldPoint, environmentLayerMask);
         return wall == null;
      }
   }
}
