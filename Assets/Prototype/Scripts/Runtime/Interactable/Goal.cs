using Prototype.Scripts.Runtime.Interfaces;
using Prototype.Scripts.Runtime.Managers;
using UnityEngine;

namespace Prototype.Scripts.Runtime.Interactable
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
