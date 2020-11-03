using UnityEngine;
using System.Collections.Generic;
using Developers.Structure;
using System;

public class AbilityCaster
{

    List<IAbility> abilitys = new List<IAbility>();


    public AbilityCaster(BaseCharacter character)
    {
        
    }

    public AbilityCaster(Building building)
    {

    }


    public void Add(IAbility ability)
    {
        if (!abilitys.Contains(ability))
        {
            abilitys.Add(ability);
        }
    }

    public void Remove(IAbility ability)
    {
        if(abilitys.Contains(ability))
        {
            abilitys.Add(ability);
        }
    }

    public IAbility Get(int index)
    {
        return abilitys.Count >= index || index < 0 ? null : abilitys[index];
    }

    public IAbility Get(Type type)
    {
        return abilitys.Find(x => x.GetType().Equals(type));
    }

    public bool HasAbility(ActorOrder order)
    {
        return abilitys.Find(x => x.Order == order) != null;
    }

    public bool HasAbility(Type type)
    {
        return abilitys.Find(x => x.GetType().Equals(type)) != null;
    }
    
    public bool OnStart(ActorOrder order)
    {
        IAbility ability = abilitys.Find(x => x.Order == order);
        if(ability == null)
        {
            return false;
        }

        return ability.OnStart();
    }

    public bool OnStart(ActorOrder order, Vector3 position)
    {
        IAbility ability = abilitys.Find(x => x.Order == order);
        if (ability == null)
        {
            return false;
        }

        return ability.OnStart(position);
    }

    public bool OnStart(ActorOrder order, IActor target)
    {
        IAbility ability = abilitys.Find(x => x.Order == order);
        if (ability == null)
        {
            return false;
        }

        return ability.OnStart(default, target);
    }
}   