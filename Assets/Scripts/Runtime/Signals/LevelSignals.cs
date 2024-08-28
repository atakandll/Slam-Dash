using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public UnityAction<byte> OnLevelInitialize = delegate { };
        public UnityAction OnClearActiveLevel = delegate { };
        public UnityAction OnLevelFailed = delegate { };
        public UnityAction OnLevelSuccess = delegate { };
        public UnityAction OnRestartLevel = delegate { };
        public UnityAction OnNextLevel = delegate { };
        
        
    }
}