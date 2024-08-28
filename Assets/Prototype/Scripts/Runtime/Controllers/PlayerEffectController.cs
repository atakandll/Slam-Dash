using System.Collections.Generic;
using Cinemachine;
using Prototype.Scripts.Runtime.Enums;
using Prototype.Scripts.Runtime.Managers;
using UnityEngine;

namespace Prototype.Scripts.Runtime.Controllers
{
    public class PlayerEffectController : MonoBehaviour
    {
        [SerializeField] private GameObject dustTrailPrefab;
        private readonly List<GameObject> _dustParticle = new List<GameObject>();
        private Animator _animator;
        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();

        }

        private void OnEnable()
        {
            GameManager.OnGameStateChange += UpdateDisplayedState;
        }

        private void UpdateDisplayedState(GameState newState) 
        {
            if (newState == GameState.Lost)
            {
                _animator.SetBool("dead",true);
            }
            else if (newState == GameState.Won)
            {
                _animator.SetBool("atGoal",true);
            }
            
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChange -= UpdateDisplayedState;
        }

        public void DustTrail(Vector3 worldPoint)
        {
            foreach (GameObject dust in _dustParticle)
            {
                if (!dust.activeInHierarchy)
                {
                    dust.transform.position = worldPoint;
                    dust.SetActive(true);
                    return;
                }
                    
            }
            var newDust = Instantiate(dustTrailPrefab, worldPoint, Quaternion.identity);
            _dustParticle.Add(newDust);
      
        }

        public void HitWall(Vector2Int impulseDirection)
        {
            Vector3 velocity = new Vector3(impulseDirection.x, impulseDirection.y, 0);
            _impulseSource.GenerateImpulseWithVelocity(velocity);
            _animator.SetTrigger("hitWall");
            // Play sound
            // flashing
        }
    }
}