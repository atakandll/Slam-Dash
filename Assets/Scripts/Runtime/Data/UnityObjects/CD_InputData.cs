using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_InputData", menuName = "Slam Dash/CD_InputData", order = 0)]
    public class CD_InputData : ScriptableObject
    {
        public InputData PlayerInputData;
    }
}