using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Interactable
{
        public class Pickup : MonoBehaviour, IInteractable
        {
                private GameManager _gameManager;

                private void Awake()
                {
                        _gameManager = FindObjectOfType<GameManager>();
                }

                public void Interact()
                {
                        _gameManager.GetExtraLife();
                        Destroy(gameObject);
                }
        }
}