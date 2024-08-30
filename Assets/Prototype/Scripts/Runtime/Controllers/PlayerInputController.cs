using UnityEngine;

namespace Prototype.Scripts.Runtime.Controllers
{
        public class PlayerInputController : MonoBehaviour
        {
                private PlayerMovementControllerDemo _playerMovementControllerDemo;
        
                private void Awake()
                { 
                        _playerMovementControllerDemo = GetComponent<PlayerMovementControllerDemo>();
                }

                private void Update()
                {
                        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                        {
                                _playerMovementControllerDemo.Move(Vector2Int.up);
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                                _playerMovementControllerDemo.Move(Vector2Int.down);
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                                _playerMovementControllerDemo.Move(Vector2Int.left);
                        }
                        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                                _playerMovementControllerDemo.Move(Vector2Int.right);
                        }
                }
        }
}