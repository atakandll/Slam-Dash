using System;
using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private PlayerManager playerManager;
       

        internal bool CheckForWall(Vector3 worldPoint)
        {
            Collider2D wall = Physics2D.OverlapPoint(worldPoint,wallLayer);

            return wall == null;
        }
    }
}