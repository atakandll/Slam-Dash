using System;
using Runtime.Controllers.UI;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables
        

        #region Serialized Variables
        
        [SerializeField] private UIPanelControllers uiPanelControllers;
        

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Start);
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnPlay += OnPlay;
            CoreGameSignals.Instance.OnReset += OnReset;
            CoreUISignals.Instance.OnClosePanel += OnClosePanel;
            CoreUISignals.Instance.OnOpenPanel += OnOpenPanel;
            LevelSignals.Instance.OnLevelInitialize += OnLevelInitialize;
        }

        private void OnLevelInitialize(byte arg0)
        {
            OnOpenPanel(UIPanelTypes.Game);
        }


        internal void ChangeStartPanel()
        {
            CoreGameSignals.Instance.OnPlay?.Invoke();
            LevelSignals.Instance.OnLevelInitialize?.Invoke(0);
        }
        private void OnPlay()
        {
            OnOpenPanel(UIPanelTypes.Game);
           OnClosePanel(UIPanelTypes.Start);
        }
        
       

        private void OnClosePanel(UIPanelTypes type) => uiPanelControllers.ChangePanel(type,false);
        private void OnOpenPanel(UIPanelTypes type) => uiPanelControllers.ChangePanel(type,true);
        
        private void OnDisable() => UnsubscribeEvents();
        
        private void UnsubscribeEvents()
        {
          CoreUISignals.Instance.OnClosePanel -= OnClosePanel;
          CoreUISignals.Instance.OnOpenPanel -= OnOpenPanel;
        }
        
        private void OnReset()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Game);
            CoreUISignals.Instance.OnClosePanel?.Invoke(UIPanelTypes.Lose);
            CoreUISignals.Instance.OnClosePanel?.Invoke(UIPanelTypes.Win);
            
        }

        

        
       
    }
}