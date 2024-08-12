using System;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers
{
    public class UILivesController : MonoBehaviour
    {
        private GameManager _gameManager;
        [SerializeField] private GameObject lifeIndicator;
        
        private void Awake() =>  _gameManager = FindObjectOfType<GameManager>();
        private void Update()
        {
            if (transform.childCount != _gameManager.GetLivesLeft())
                RefreshChildren();
          
        }

        private void RefreshChildren()
        {
            // delete all children
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            //add the right number back

            for (int i = 0; i < _gameManager.GetLivesLeft(); i++)
            {
                Instantiate(lifeIndicator, transform);
            }
        }
    }
}