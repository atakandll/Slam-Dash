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
        
        private void OnEnable()
        {
            GameManager.OnGameStateChange += UpdateDisplayedState;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChange -= UpdateDisplayedState;
        }

        private void UpdateDisplayedState(GameState newState)
        {
            if (newState == GameState.Lost)
            {
                losePanel.SetActive(true);
                winPanel.SetActive(false);
            }
            else if (newState == GameState.Won)
            {
                winPanel.SetActive(true);
                losePanel.SetActive(false);
            }
            else if (newState == GameState.Playing)
            {
                losePanel.SetActive(false);
                winPanel.SetActive(false);

            }
        }
    }
}