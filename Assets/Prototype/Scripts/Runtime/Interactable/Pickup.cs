using Prototype.Scripts.Runtime.Interfaces;
using Prototype.Scripts.Runtime.Managers;
using UnityEngine;

namespace Prototype.Scripts.Runtime.Interactable
{
        public class Pickup : MonoBehaviour, IInteractable
        {
                private GameManager _gameManager;
                
                public void Interact()
                {
                        GameManager.Instance.GetExtraLife();
                        Destroy(gameObject);
                }
        }
}