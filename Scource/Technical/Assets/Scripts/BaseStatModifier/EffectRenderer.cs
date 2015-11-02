using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectRenderer : MonoBehaviour {

    private List<StatModifier> statModifierStocks;

    void Awake()
    {
        statModifierStocks = new List<StatModifier>();
    }

    public float GetStatModifier(BaseStatModifierType _statModifierType){

        StatModifier statModifier = statModifierStocks.Find(x => x.stateModifierType == _statModifierType);
        if (statModifier != null)
        {
            return statModifier.param;
        }
        return -1;
    }

    public bool Contains(BaseStatModifierType _statModifierType)
    {
        return statModifierStocks.Exists(x=>x.stateModifierType == _statModifierType);
    }

    public void AddStatModifier(BaseStatModifierType _statModifierType, float _timeExpired, float _param)
    {
        StatModifier statModifier;
        if (Contains(_statModifierType))
        {
            statModifier = statModifierStocks.Find(x => x.stateModifierType == _statModifierType);
        }
        else
        {
            statModifier = new StatModifier();
        }
        statModifier.timeExpired = _timeExpired + Time.time;
        statModifier.stateModifierType = _statModifierType;
        statModifier.param = _param;
        //
        statModifierStocks.Add(statModifier);

    }

    public bool RemoveStatModifier(BaseStatModifierType _statModifierType)
    {
        StatModifier statModifier = statModifierStocks.Find(x => x.stateModifierType == _statModifierType);
        if (statModifier != null && Time.time >= statModifier.timeExpired)
        {
            statModifierStocks.Remove(statModifier);
            return true;
        }
        return false;
    }


}
