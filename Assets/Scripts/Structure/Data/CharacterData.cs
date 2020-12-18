using UnityEngine;
using System.Collections.Generic;
using Developers.Table;

namespace Developers.Structure.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public int index;
        public Sprite illust;

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


        private void Load ( TableManager table )
        {
            var characterInfoSheet = table.CharacterTable.CharacterInfoSheet;
            var record = BaseTable.Get ( characterInfoSheet, "index", index );

            Name = (string)record["name"];
            Cost = (int)record["name"];
            Ability = (List<int>)record["name"];
            Atk = (float)record["name"];
            Def = (float)record["name"];
            HP = (float)record["name"];
            Speed = (float)record["name"];
            AtkCooltime = (float)record["name"];
            Distance = (float)record["name"];

            isLoad = true;
        }


        private void Awake ( )
        {
            TableManager.Instance.Record ( Load );
        }
    }
}