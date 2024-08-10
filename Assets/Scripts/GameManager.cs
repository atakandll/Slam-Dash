using UnityEngine;

public class GameManager : MonoBehaviour
{
        [SerializeField] private int lives;
        private int _turnCount = 0;

        public void TurnTaken()
        {
                _turnCount++;
                
                if (_turnCount == lives)
                {
                        Debug.Log("Game Over");
                        
                }

        }

        public void Win()
        {
                Debug.Log("You Win");
                
        }
}