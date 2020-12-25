using UnityEngine;
using System.Collections.Generic;
using Spine.Unity;

namespace Developers.Structure.Data
{
    using Table;

    [CreateAssetMenu (fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public int index;
        public Sprite illust;
        public SkeletonDataAsset asset;

        public Race Race {
            get; private set;
        }

        public string Name {
            get; private set;
        }

        public int Cost {
            get; private set;
        }

        public List<int> Ability { 
            get; private set; 
        }

        public float Atk {
            get; private set;
        }

        public float Def {
            get; private set;
        }

        public float HP {
            get; private set;
        }

        public float Speed {
            get; private set;
        }

        public float AtkCooltime {
            get; private set;
        }

        public float Distance {
            get; private set;
        }

        public bool isLoad = false;


        void Load ( TableManager table )
        {
            if ( isLoad == true )
            {
                return;
            }

            var characterInfoSheet = table.CharacterTable.CharacterInfoSheet;
            var record = BaseTable.Get ( characterInfoSheet, "index", index );

            Race = (Race)( (int)record["race"] );
            Name = (string)record["name"];
            Cost = (int)record["cost"];
            Ability = BaseTable.ListParsing<int> ( (List<object>)record["ability"] );
            Atk = (float)record["atk"];
            Def = (float)record["def"];
            HP = (float)record["hp"];
            Speed = (float)record["speed"];
            AtkCooltime = (float)record["atkCooltime"];
            Distance = (float)record["distance"];

            isLoad = true;
        }

        public void Initialize()
        {
            isLoad = false;
            TableManager.Instance.Record ( Load );
        }
    }
}