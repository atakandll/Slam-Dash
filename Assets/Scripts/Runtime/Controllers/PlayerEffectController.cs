using System;
using System.Collections.Generic;
using Cinemachine;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Controllers
{
    public class PlayerEffectController : MonoBehaviour
    {
        [SerializeField] private GameObject dustTrailPrefab;
        private readonly List<GameObject> _dustParticle = new List<GameObject>();
        private Animator _animator;
        private GameManager _gameManager;
        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _gameManager = FindObjectOfType<GameManager>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();

        }

        private void Update()
        {
            if(_gameManager.GetGameState() == GameState.Lost)
            {
                _animator.SetBool("dead",true);
            }
            else if(_gameManager.GetGameState() == GameState.Won)
            {
                _animator.SetBool("atGoal",true);
            }
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