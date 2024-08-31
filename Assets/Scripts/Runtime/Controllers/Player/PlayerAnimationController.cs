using System;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
       [SerializeField] private Animator animator;
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState += OnChangePlayerAnimation;
        }

        private void OnChangePlayerAnimation(PlayerAnimationStates states)
        {
            animator.SetTrigger(states.ToString());
        }

        private void OnDisable() => UnsubscribeEvents();
      
        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState -= OnChangePlayerAnimation;
        }
        
        internal void OnReset()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
        }
    }
}