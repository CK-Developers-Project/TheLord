using UnityEngine;
using System.Collections.Generic;
using Developers.Structure;
using System;

public class AbilityCaster
{

    List<BaseAbility> abilitys = new List<BaseAbility>();


    public AbilityCaster(BaseCharacter character)
    {
        
    }

    public AbilityCaster(Building building)
    {

    }


    public void Add(BaseAbility ability)
    {
        if (!abilitys.Contains(ability))
        {
            abilitys.Add(ability);
        }
    }

    public void Remove(BaseAbility ability)
    {
        if(abilitys.Contains(ability))
        {
            abilitys.Add(ability);
        }
    }

    public BaseAbility Get(int index)
    {
        return abilitys.Count >= index || index < 0 ? null : abilitys[index];
    }

    public BaseAbility Get(Type type)
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
        BaseAbility ability = abilitys.Find(x => x.Order == order);
        if(ability == null)
        {
            return false;
        }

        return ability.OnStart();
    }

    public bool OnStart(AbilityOrder order, Vector3 position)
    {
        BaseAbility ability = abilitys.Find(x => x.Order == order);
        if (ability == null)
        {
            return false;
        }

        return ability.OnStart(position);
    }

    public bool OnStart(AbilityOrder order, IActor target)
    {
        BaseAbility ability = abilitys.Find(x => x.Order == order);
        if (ability == null)
        {
            return false;
        }

        return ability.OnStart(default, target);
    }
}   