using UnityEngine;

namespace Developers.Structure.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Data")]
    public class CharacterData : ScriptableObject
    {
        public int index;

        /*
        FIXME
        현재 테이블값을 가져오지 못함으로 여기에 우선 작성하고 넣도록 하자
        추후 테이블 작성을 한 후 Server로부터 Table Structure를 가져와서 Load시켜야함!
        */
        public new string name;
        public ActorAbility ability = new ActorAbility();
        /* */
    }
}