using UnityEngine;

namespace Developers.Structure.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public int index;

        public CharacterInfo Info { get; private set; }

        /*
        FIXME
        현재 테이블값을 가져오지 못함으로 여기에 우선 작성하고 넣도록 하자
        추후 테이블 작성을 한 후 Server로부터 Table Structure를 가져와서 Load시켜야함!
        */
        public int price;
        public new string name;

        public float damage;
        public float armor;
        public float life;
        public float speed;
        public float aspeed;
        /* */


        public CharacterInfo GetInfo ( )
        {
            CharacterInfo info = new CharacterInfo ( );
            info.name = Info.name;

            return info;
        }


        void Initialize()
        {
            /*
            [FIXME] 임시 데이터 테이블 값
            */
            Info = new CharacterInfo ( );
            Info.index = index;
            Info.price = price;
            Info.name = name;

            Info.damage = damage;
            Info.armor = armor;
            Info.life = life;
            Info.speed = speed;
            Info.aspeed = aspeed;
            // [TODO] . . .
        }


        void Awake()
        {
            Initialize ( );
        }
    }
}