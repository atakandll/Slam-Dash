using Runtime.Managers;
using UnityEngine;

namespace Runtime.Interactable
{
    public class Goal : MonoBehaviour
    {
        private GameManager _gameManager;
        private void Awake() => _gameManager = FindObjectOfType<GameManager>();
        public void PlayerAtGoal() => _gameManager.Win();
    }
}