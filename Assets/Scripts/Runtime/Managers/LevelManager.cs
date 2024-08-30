using Runtime.Commands.Level;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] internal GameObject levelHolder;
        [SerializeField] private byte totalLeventCount;


        #endregion

        #region Private Variables

        private LevelLoaderCommand _levelLoader;
        private LevelDestroyerCommand _levelDestroyer;

        private byte _currentLevel;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _levelLoader = new LevelLoaderCommand(this);
            _levelDestroyer = new LevelDestroyerCommand(this);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.OnLevelInitialize += _levelLoader.Execute;
            LevelSignals.Instance.OnClearActiveLevel += _levelDestroyer.Execute;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.OnLevelInitialize -= _levelLoader.Execute;
            LevelSignals.Instance.OnClearActiveLevel -= _levelDestroyer.Execute;
        }

        private void Start()
        {
            LevelSignals.Instance.OnLevelInitialize?.Invoke(GetLevelID());
        }

        private byte GetLevelID()
        {
            return _currentLevel;


        }

        private void OnDestroy()
        {
            _levelLoader.Undo();
        }
    }
}
