using Runtime.Managers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.Commands
{
    public class LevelLoaderCommand
    {
        private AsyncOperationHandle _request;
        private readonly LevelManager _levelManager;

        public LevelLoaderCommand(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }
        
        public void Execute(byte levelIndex)
        {
            _request = Addressables.LoadAssetAsync<GameObject>($"Prefabs/LevelPrefabs/Level {levelIndex}");
            _request.Completed += handle =>
            {
                var newLevel = Object.Instantiate(_request.Result as GameObject, Vector3.zero, Quaternion.identity);
                if(newLevel!=null) newLevel.transform.SetParent(_levelManager.levelHolder.transform);

            };

        }

        public void Undo()
        {
            Addressables.Release(_request);
        }
        
       
    }
}