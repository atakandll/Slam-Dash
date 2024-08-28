using Prototype.Scripts.Runtime.Managers;
using UnityEngine;

namespace Prototype.Scripts.Runtime.Controllers
{
    public class UILivesController : MonoBehaviour
    {
        
        [SerializeField] private GameObject lifeIndicator;
        
       
        private void Update()
        {
            if (transform.childCount != GameManager.Instance.GetLivesLeft())
                RefreshChildren();
          
        }

        private void RefreshChildren()
        {
            // delete all children
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            //add the right number back

            for (int i = 0; i < GameManager.Instance.GetLivesLeft(); i++)
            {
                Instantiate(lifeIndicator, transform);
            }
        }
    }
}