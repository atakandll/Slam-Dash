using UnityEngine;

namespace Runtime.Controllers
{
    public class PlayerEffectController : MonoBehaviour
    {
        [SerializeField] private GameObject dustTrailPrefab;
        public void DustTrail(Vector3 worldPoint)
        {
            Instantiate(dustTrailPrefab, worldPoint, Quaternion.identity);
      
        }

        public void HitWall()
        {
            // Play sound
            // flashing
        }
    }
}