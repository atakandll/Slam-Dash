﻿using System;
using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<PlayerAnimationStates> onChangePlayerAnimationState = delegate { };
        public UnityAction<bool> onMoveConditionChanged = delegate { };
        public UnityAction<Vector2Int> onPlayerMove = delegate { };
        
    }
}