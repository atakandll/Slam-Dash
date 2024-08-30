using Runtime.Custom;
using Runtime.Data.UnityObjects;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables


        #region Serialized Variables

        [SerializeField] private AssetReferenceScriptableObject data;


        #endregion

        #region Private Variables

        private bool _isAvailableForInput;
        private CD_InputData  _data;


        #endregion

        #endregion

        private void Awake() => GetData();
      
        #region Event Subscriptions

        private void OnEnable() => SubscribeEvents();
        
        private void SubscribeEvents()
        {
            InputSignals.Instance.OnInputStateChanged += OnInputStateChanged;
        }

        private void OnInputStateChanged(bool state)
        {
            _isAvailableForInput = state;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
            Addressables.Release(data);
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.OnInputStateChanged -= OnInputStateChanged;
        }

        #endregion

        private void Update()
        {
            //Todo : Refactoring with command pattern
            
            if (_isAvailableForInput)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Debug.Log("W" + _data.PlayerInputData);
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("S");
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Debug.Log("A");
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Debug.Log("D");
                }
            }
        }


        #region AssetReference
        private void GetData()
        {
            var request = Addressables.LoadAssetAsync<ScriptableObject>(data);
            request.Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("<color=green>ScriptableObject loaded!</color>");
                    _data = handle.Result as CD_InputData;

                }
                else
                {
                    Debug.LogError("ScriptableObject not found!");
                }
            };
            
        }

        #endregion
        
    }
}