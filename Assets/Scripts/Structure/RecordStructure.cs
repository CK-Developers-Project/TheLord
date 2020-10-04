using System;

namespace Developers.Structure
{
    /// <summary>캐릭터 테이블 자료구조</summary>
    [Serializable]
    public class CharacterRecord
    {
        public int index;
        public string name;
        public int code;
        public int type;
        public int cost;
        public int speed;

        public CharacterRecord ( ) { }
        public CharacterRecord(CharacterRecord record)
        {
            index = record.index;
            name = record.name;
            code = record.code;
            type = record.type;
            cost = record.cost;
            speed = record.speed;
        }
    }


}
