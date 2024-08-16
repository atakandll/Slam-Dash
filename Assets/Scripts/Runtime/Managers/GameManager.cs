using System;
using Runtime.Data;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Managers
{
        public class GameManager : MonoBehaviour
        {
                #region Singleton

                private static GameManager _instance;
                public static GameManager Instance
                {
                        get
                        {
                                if (_instance == null)
                                {
                                        _instance = FindObjectOfType<GameManager>();
                                        if (_instance == null)
                                        {
                                                GameObject obj = new GameObject();
                                                _instance = obj.AddComponent<GameManager>();
                                        }
                                }
                                return _instance;
               
                        }
                }
                private void Awake()
                {
                        Application.targetFrameRate = 60;

                        _instance = this as GameManager;
                }

                #endregion
                
                public static Action<GameState> OnGameStateChange;
                [SerializeField] private CD_LevelList levelList;
                [SerializeField] private int lives;
                private int _turnCount = 0;
                private GameState gameState;
                
                private void Start()
                {
                        ChangeGameState(GameState.Playing);
                }

                private void Update()
                {
                        // for testing purposes
            
                        if (Input.GetKeyDown(KeyCode.R))
                        {
                                levelList.RestartLevel();
                        }

                        if (Input.GetKeyDown(KeyCode.N))
                        {
                                levelList.GoNextLevel();
                        }
                }

                public void GetExtraLife()
                {
                        _turnCount--;
                }

                private void ChangeGameState(GameState newState)
                {
                        gameState = newState;
                        OnGameStateChange?.Invoke(newState);
                        
                }
                public void TurnTaken()
                {
                        _turnCount++;
                
                        if (_turnCount == lives)
                        {
                                Lose();
                        }
                }

                public int GetLivesLeft() => lives - _turnCount;
             
                public GameState GetGameState() => gameState;
                public void Win()
                {
                        ChangeGameState(GameState.Won);

                }

                public void Lose()
                {
                        if (gameState != GameState.Won)
                        {
                                ChangeGameState(GameState.Lost);
                        
                        } 
                } 
        }
}