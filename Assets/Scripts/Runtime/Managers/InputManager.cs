using System;
using Runtime.Commands.Movement;
using Runtime.Custom;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Interface;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables


        #region Serialized Variables

        [SerializeField] private AssetReferenceT<CD_InputData> data;
        
        #endregion

        #region Private Variables

        private bool _isAvailableForInput;
        private IMoveCommand moveLeftCommand;
        private IMoveCommand moveRightCommand;
        private IMoveCommand moveUpCommand;
        private IMoveCommand moveDownCommand;
        private InputData  _data;


        #endregion

        #endregion

        private void Awake()
        {
            GetData();
            Init();
        }

        private void Init()
        {
            moveDownCommand = new MoveDownCommand();
            moveLeftCommand = new MoveLeftCommand();
            moveRightCommand = new MoveRightCommand();
            moveUpCommand = new MoveUpCommand();
        }

        #region Event Subscriptions

        private void OnEnable() => SubscribeEvents();
        
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnPlay += OnPlay;
            CoreGameSignals.Instance.OnReset += OnReset;
            
        }

        private void OnReset() =>  _isAvailableForInput = false;
        private void OnPlay() => _isAvailableForInput = true;
        

        private void OnDisable() =>  UnsubscribeEvents();
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.OnPlay -= OnPlay;
            CoreGameSignals.Instance.OnReset -= OnReset;
        }

        #endregion

        private void Update()
        {
            
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    moveUpCommand.Execute();
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    moveDownCommand.Execute();
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    moveLeftCommand.Execute();
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    moveRightCommand.Execute();
                }
                
           
        }


        #region AssetReference
        private void GetData()
        {
            var request = data.LoadAssetAsync();
            request.Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("<color=green>ScriptableObject loaded!</color>");
                    _data = handle.Result.PlayerInputData;
                }
                else Debug.LogError("ScriptableObject not found!");
                    
                
            };
        }

        private void OnDestroy() =>  Addressables.Release(data);
       
        #endregion
        
    }
}