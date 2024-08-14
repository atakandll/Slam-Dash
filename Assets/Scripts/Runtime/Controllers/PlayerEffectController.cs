using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Controllers
{
    public class PlayerEffectController : MonoBehaviour
    {
        [SerializeField] private GameObject dustTrailPrefab;
        private readonly List<GameObject> _dustParticle = new List<GameObject>();
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

        public void HitWall()
        {
            // Play sound
            // flashing
        }
    }
}