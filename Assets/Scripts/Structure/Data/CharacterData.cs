using UnityEngine;

namespace Developers.Structure.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Data")]
    public class CharacterData : ScriptableObject
    {
        public CharacterInfo Info;
        [Header("임시용 데이터"), Tooltip("캐릭터 프리펩에 포함되어있는 데이터들인데 현재는 없기 때문에 여기에 포함시킴")]
        public string rename;
    }
}