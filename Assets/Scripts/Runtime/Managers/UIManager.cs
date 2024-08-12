using System;
using Runtime.Enums;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;
        private GameManager _gameManager;
        private GameState currentlyDisplayedState;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start() => UpdateDisplayedState();
       
        private void Update()
        {
            if(currentlyDisplayedState != _gameManager.GetGameState()) 
                UpdateDisplayedState();
            
        }

        private void UpdateDisplayedState()
        {
            if (_gameManager.GetGameState() == GameState.Lost)
            {
                losePanel.SetActive(true);
                winPanel.SetActive(false);
                currentlyDisplayedState = GameState.Lost;
            }
            else if (_gameManager.GetGameState() == GameState.Won)
            {
                winPanel.SetActive(true);
                losePanel.SetActive(false);
                currentlyDisplayedState = GameState.Won;
            }
            else if (_gameManager.GetGameState() == GameState.Playing)
            {
                losePanel.SetActive(false);
                winPanel.SetActive(false);
                currentlyDisplayedState = GameState.Playing;
                
            }
        }
    }
}