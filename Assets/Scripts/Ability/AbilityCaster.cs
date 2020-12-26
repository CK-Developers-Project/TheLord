using UnityEngine;
using System.Collections.Generic;
using Developers.Structure;
using System;

[Serializable]
public class AbilityCaster
{
    [SerializeField] List<AbilityInfo> abilitys = new List<AbilityInfo> ( );

    public bool abilityCast = false;
    public AbilityOrder order = AbilityOrder.Idle;
    public BaseCharacter Owner { get; set; }

    public AbilityInfo this[int value] 
    {
        get
        {
            return abilitys.Count >= value || value < 0 ? null : abilitys[value];
        }
    }


    public void Add(AbilityInfo ability)
    {
        if (!abilitys.Contains(ability))
        {
            abilitys.Add(ability);
        }
    }

    public void Remove(AbilityInfo ability)
    {
        if(abilitys.Contains(ability))
        {
            abilitys.Add(ability);
        }
    }

    public AbilityInfo Get(int index)
    {
        return abilitys.Find ( x => x.Data.index == index );
    }

    public AbilityInfo Get(Type type)
    {
        return abilitys.Find(x => x.GetType().Equals(type));
    }

    public bool HasAbility(AbilityOrder order)
    {
        return abilitys.Find(x => x.Order == order) != null;
    }

    public bool HasAbility(Type type)
    {
        return abilitys.Find(x => x.GetType().Equals(type)) != null;
    }
    
    public bool OnStart(AbilityOrder order)
    {
        if( abilityCast )
        {
            return false;
        }
        AbilityInfo ability = abilitys.Find(x => x.Order == order);
        if(ability == null)
        {
            return false;
        }

        if ( ability.isUse || ability.cooltime > 0)
        {
            return false;
        }

        ability.isUse = ability.OnStart ( Owner );
        return ability.isUse;
    }

    public bool OnStart(AbilityOrder order, Vector3 position)
    {
        if ( abilityCast )
        {
            return false;
        }
        AbilityInfo ability = abilitys.Find(x => x.Order == order);
        if (ability == null)
        {
            return false;
        }

        if ( ability.isUse || ability.cooltime > 0 )
        {
            return false;
        }

        ability.isUse = ability.OnStart ( Owner, position );
        return ability.isUse;
    }

    public bool OnStart(AbilityOrder order, IActor target)
    {
        if ( abilityCast )
        {
            return false;
        }
        AbilityInfo ability = abilitys.Find(x => x.Order == order);
        if (ability == null)
        {
            return false;
        }

        if ( ability.isUse || ability.cooltime > 0 )
        {
            return false;
        }

        ability.isUse = ability.OnStart ( Owner, target );
        return ability.isUse;
    }
}   