using UnityEngine;

namespace Developers.Structure.Data
{
    [CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects/MusicData" )]
    public class MusicData : ScriptableObject
    {
        public MusicType type;
        public AudioClip clip;
    }
}
