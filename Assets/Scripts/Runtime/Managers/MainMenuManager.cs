using System;
using Runtime.Data;
using UnityEngine;

namespace Runtime.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private CD_LevelList levelList;

        private void Start()
        {
            levelList.StartGame();
        }
    }
}