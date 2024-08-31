using System;
using Runtime.Controllers.Player;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerPhysicController playerPhysicController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerFeelController playerFeelController;

        

        #endregion

        #region Private Variables

        private bool _isAvailableForInput;

        #endregion
        

        #endregion

        private void OnEnable() => SubscribeEvents();
       
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnPlay += OnPlay;
            CoreGameSignals.Instance.OnReset += OnReset;
            LevelSignals.Instance.OnLevelFailed += OnLevelFailed;
            LevelSignals.Instance.OnLevelSuccess += OnLevelSuccess;
        }

       

        private void OnLevelSuccess()
        {
            PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);

        }

        private void OnLevelFailed()
        {
            PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);

        }

        private void OnReset()
        {
            PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);

        }

        private void OnPlay()
        {
           PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
           PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
        }

        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.OnPlay -= OnPlay;
            CoreGameSignals.Instance.OnReset -= OnReset;
            LevelSignals.Instance.OnLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.OnLevelSuccess -= OnLevelSuccess;
        }

        
        
        internal bool CheckForWallForMovement(Vector3 worldPoint)
        {
            return playerPhysicController.CheckForWall(worldPoint);            
        }
    }
}