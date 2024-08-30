using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototype.Scripts.Runtime.Data
{
    [CreateAssetMenu(fileName = "CD_LevelList", menuName = "Prototype/Data/CD_LevelList", order = 0)]
    public class CD_LevelList : ScriptableObject
    {
        public int CurrentLevel;
        public List<string> Levels;
        
        public void GoNextLevel()
        {
            CurrentLevel++;

            if (CurrentLevel >= Levels.Count)
            {
                // you won
                CurrentLevel = 0;

            }
            SceneManager.LoadScene(Levels[CurrentLevel]);

        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
        }

        public void StartGame()
        {
            CurrentLevel = 0;
            SceneManager.LoadScene(Levels[0]);

        }
    }
}