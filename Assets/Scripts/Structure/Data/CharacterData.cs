using UnityEngine;

namespace Developers.Structure.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public int index;

        public CharacterInfo Info { get; private set; }


        public CharacterInfo GetInfo ( )
        {
            CharacterInfo info = new CharacterInfo ( );
            info.name = Info.name;

            return info;
        }


        void Initialize()
        {
            
        }


        void Awake()
        {
            Initialize ( );
        }
    }
}