using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Controllers
{
    public class PlayerEffectController : MonoBehaviour
    {
        [SerializeField] private GameObject dustTrailPrefab;
        private List<GameObject> _dustParticle = new List<GameObject>();
        public void DustTrail(Vector3 worldPoint)
        {
            foreach (GameObject g in _dustParticle)
            {
                if (!g.activeInHierarchy)
                {
                    g.transform.position = worldPoint;
                    g.SetActive(true);
                    return;
                }
                    
            }
            GameObject newDust = Instantiate(dustTrailPrefab, worldPoint, Quaternion.identity);
            _dustParticle.Add(newDust);
      
        }

        public void HitWall()
        {
            // Play sound
            // flashing
        }
    }
}