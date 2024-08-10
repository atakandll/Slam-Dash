using System;
using Enums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        [SerializeField] private int lives;
        private int _turnCount = 0;
        private GameState gameState;

        private void Start()
        {
                gameState = GameState.Playing;
        }


        public void TurnTaken()
        {
                _turnCount++;
                
                if (_turnCount == lives)
                {
                        Lose();
                }
                
                

        }
        public GameState GetGameState()
        {
                return gameState;
        }
        

        public void Win()
        {
                gameState = GameState.Won;
                Debug.Log("You Win");
                
        }

        public void Lose()
        {
                if (gameState != GameState.Won)
                {
                        gameState = GameState.Lost;
                        Debug.Log("Game Over");
                        
                } 
        } 
}