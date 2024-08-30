using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction OnPlay = delegate { };
        public UnityAction OnReset = delegate { };
        
    }
}