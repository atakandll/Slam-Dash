using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
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

        [SerializeField] private AssetReferenceT<CD_InputData> data;
        
        #endregion

        #region Private Variables

        private bool _isAvailableForInput;
        
        private InputData  _data;
        private Vector2Int? direction = null;


        #endregion

        #endregion

        private void Awake()
        {
            GetData();
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
            //if (!_isAvailableForInput) return;
            //if (!Input.anyKey) return;
    
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                direction = Vector2Int.up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                direction = Vector2Int.down;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                direction = Vector2Int.left;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                direction = Vector2Int.right;

            if (direction.HasValue)
                PlayerSignals.Instance.onPlayerMove?.Invoke(direction.Value);
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