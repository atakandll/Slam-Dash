using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelControllers : MonoBehaviour
    {
        [SerializeField] private List<GameObject> uiPanels = new List<GameObject>();

        internal void ChangePanel(UIPanelTypes type, bool status)
        {
            uiPanels[(int) type].SetActive(status);
        }
    }
}