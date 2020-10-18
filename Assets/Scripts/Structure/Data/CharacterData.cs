using UnityEngine;

namespace Developers.Structure.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Data")]
    public class CharacterData : ScriptableObject
    {
        public CharacterInfo Info;
        public int index;
    }
}