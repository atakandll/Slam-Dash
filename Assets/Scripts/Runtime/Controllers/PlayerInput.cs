using UnityEngine;

namespace Runtime.Controllers
{
        public class PlayerInput : MonoBehaviour
        {
                private PlayerMovement _playerMovement;
        
                private void Awake()
                { 
                        _playerMovement = GetComponent<PlayerMovement>();
                }

                private void Update()
                {
                        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                        {
                                _playerMovement.Move(Vector2Int.up);
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                                _playerMovement.Move(Vector2Int.down);
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                                _playerMovement.Move(Vector2Int.left);
                        }
                        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                                _playerMovement.Move(Vector2Int.right);
                        }
                }
        }
}