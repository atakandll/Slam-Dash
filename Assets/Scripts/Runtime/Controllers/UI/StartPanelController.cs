using Runtime.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.UI
{
    public class StartPanelController : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Button startButton;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            startButton.onClick.AddListener(() => uiManager.ChangeStartPanel());
        }
    }
}