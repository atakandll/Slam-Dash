using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
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
      //todo : Sanitize input
      MoveOnceRecursive(direction);
      gameManager.TurnTaken();
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
      Collider2D overlap = Physics2D.OverlapPoint((Vector2)worldPoint, interactableLayerMask);

      if (overlap != null)
      {
         // Interact with pickup
         Interactable interactable = overlap.GetComponent<Interactable>();

         if (interactable != null)
         {
            interactable.Interact();
         }
        

      }
      
      

   }

   private void SpawnDustTrail(Vector3 worldPoint)
   {
      Instantiate(dustTrailPrefab, worldPoint, Quaternion.identity);
      
   }

   private bool IsNoWallHere(Vector3 worldPoint)
   {
      Collider2D wall = Physics2D.OverlapPoint((Vector2)worldPoint, environmentLayerMask);
      return wall == null;
   }
}
