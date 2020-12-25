using System.Collections.Generic;
using UnityEngine;

namespace Developers.Structure.Data
{
    using Table;

    public class AbilityData : ScriptableObject
    {
        public int index;
        public AbilityOrder order;
        public Sprite icon;

        public string Name {
            get; private set;
        }

        public string Descript {
            get; private set;
        }

        public AbilityType AbilityType {
            get; private set;
        }

        public float Cast {
            get; private set;
        }

        public float Duration {
            get; private set;
        }

        public float Range {
            get; private set;
        }

        public float Distance {
            get; private set;
        }

        public float Cooltime {
            get; private set;
        }

        public List<int> Amount {
            get; private set;
        }

        public bool isLoad = false;


        public virtual bool OnStart ( BaseCharacter owner, AbilityInfo info ) { return false; }
        public virtual bool OnStart ( BaseCharacter owner, AbilityInfo info, Vector3 position ) { return false; }
        public virtual bool OnStart ( BaseCharacter owner, AbilityInfo info, IActor target ) { return false; }

        public virtual void Effect ( ) { }

        public void Add(AbilityCaster caster)
        {
            if ( caster.HasAbility ( order ) )
            {
                return;
            }
            caster.Add ( new AbilityInfo ( this ) );
        }

        void Load ( TableManager table )
        {
            if ( isLoad == true )
            {
                return;
            }

            var abilityInfoSheet = table.AbilityTable.AbilityInfoSheet;
            var record = BaseTable.Get ( abilityInfoSheet, "index", index );

            Name = (string)record["name"];
            Descript = (string)record["descript"];
            AbilityType = (AbilityType)(int)record["abilityType"];
            Cast = (float)record["cast"];
            Duration = (float)record["duration"];
            Range = (float)record["range"];
            Distance = (float)record["distance"];
            Cooltime = (float)record["cooltime"];
            Amount = BaseTable.ListParsing<int> ( (List<object>)record["amount"] );

            isLoad = true;
        }


        public void Initialize ( )
        {
            isLoad = false;
            TableManager.Instance.Record ( Load );
        }
    }
}
