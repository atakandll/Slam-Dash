using Runtime.Enums;
using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreUISignals : MonoSingleton<CoreUISignals>
    {
        public UnityAction<UIPanelTypes> OnOpenPanel = delegate {  };

        public UnityAction<UIPanelTypes> OnClosePanel = delegate {  };
        
        
    }
}