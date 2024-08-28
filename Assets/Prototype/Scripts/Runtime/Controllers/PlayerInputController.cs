using UnityEngine;

namespace Prototype.Scripts.Runtime.Controllers
{
        public class PlayerInputController : MonoBehaviour
        {
                private PlayerMovementController _playerMovementController;
        
                private void Awake()
                { 
                        _playerMovementController = GetComponent<PlayerMovementController>();
                }

                private void Update()
                {
                        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                        {
                                _playerMovementController.Move(Vector2Int.up);
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                                _playerMovementController.Move(Vector2Int.down);
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                                _playerMovementController.Move(Vector2Int.left);
                        }
                        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                                _playerMovementController.Move(Vector2Int.right);
                        }
                }
        }
}