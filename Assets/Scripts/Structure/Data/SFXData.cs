using UnityEngine;

namespace Developers.Structure.Data
{
    [CreateAssetMenu ( fileName = "SFXData", menuName = "ScriptableObjects/SFXData" )]
    public class SFXData : ScriptableObject
    {
        public SFXType type;
        public AudioClip clip;
    }
}
