using UnityEngine;

namespace Developers.Structure.Data
{
    [CreateAssetMenu ( fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData" )]
    public class AbilityData : ScriptableObject
    {
        public AbilityOrder order;

        public float cast;
        public float duration;
        public float range;
        public float distance;
        public float amount;
    }
}