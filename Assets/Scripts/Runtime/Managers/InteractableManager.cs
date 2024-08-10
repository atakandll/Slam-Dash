using Runtime.Interactable;
using UnityEngine;

namespace Runtime.Managers
{
   public class InteractableManager : MonoBehaviour
   {
      private Goal _goal;
      private Pickup _pickup;

      private void Awake()
      {
         _goal = GetComponent<Goal>();
         _pickup = GetComponent<Pickup>();
      }
   
   

      public void Interact()
      {
         if (_goal != null)
         {
            _goal.PlayerAtGoal();
         }

         if (_pickup != null)
         {
            _pickup.PickUp();
         }
         // Do whatever
         Destroy(gameObject);
      }
   }
}
