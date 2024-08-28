using Prototype.Scripts.Runtime.Data;
using UnityEngine;

namespace Prototype.Scripts.Runtime.Managers
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