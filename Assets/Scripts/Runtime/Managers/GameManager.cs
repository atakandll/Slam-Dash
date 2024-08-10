using Runtime.Enums;
using UnityEngine;

namespace Runtime.Managers
{
        public class GameManager : MonoBehaviour
        {
                [SerializeField] private int lives;
                private int _turnCount = 0;
                private GameState gameState;

                private void Start() => gameState = GameState.Playing;
                public void TurnTaken()
                {
                        _turnCount++;
                
                        if (_turnCount == lives)
                        {
                                Lose();
                        }
                }

                public int GetLives() => lives - _turnCount;
             
                public GameState GetGameState() => gameState;
                public void Win() =>  gameState = GameState.Won;
              
                public void Lose()
                {
                        if (gameState != GameState.Won)
                        {
                                gameState = GameState.Lost;
                                Debug.Log("Game Over");
                        
                        } 
                } 
        }
}