using Runtime.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Interactable
{
    public class Goal : MonoBehaviour, IInteractable
    {
        private GameManager _gameManager;
        
        public void Interact()
        {
            GameManager.Instance.Win();
        }
    }
    
}
