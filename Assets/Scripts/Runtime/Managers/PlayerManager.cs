using System;
using Runtime.Controllers.Player;
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

        

        #endregion

        #region Private Variables

        private bool _isAvailableForInput;

        #endregion

        

        #endregion

        private void OnEnable() => SubscribeEvents();
       
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnPlay += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            CoreGameSignals.Instance.OnReset += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            LevelSignals.Instance.OnLevelFailed += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            LevelSignals.Instance.OnLevelSuccess += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
        }
        private void OnPlay()
        {
           
        }
        private void OnReset()
        {
            
        }

        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.OnPlay -= OnPlay;
            CoreGameSignals.Instance.OnReset -= OnReset;
            LevelSignals.Instance.OnLevelFailed -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            LevelSignals.Instance.OnLevelSuccess -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
        }


        internal bool CheckForWallForMovement(Vector3 worldPoint)
        {
            return playerPhysicController.CheckForWall(worldPoint);            
        }
    }
}