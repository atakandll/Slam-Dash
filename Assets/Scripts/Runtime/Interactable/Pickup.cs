using UnityEngine;

namespace Runtime.Interactable
{
        public class Pickup : MonoBehaviour
        {
                public void PickUp()
                {
                        Destroy(gameObject);
                }
        }
}