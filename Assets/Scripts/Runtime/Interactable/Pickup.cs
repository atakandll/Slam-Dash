using Runtime.Managers;
using UnityEngine;

namespace Runtime.Interactable
{
        public class Pickup : MonoBehaviour
        {
                private GameManager _gameManager;

                private void Awake()
                {
                        _gameManager = FindObjectOfType<GameManager>();
                }

                public void PickUp()
                {
                        _gameManager.GetExtraLife();
                        Destroy(gameObject);
                }
        }
}