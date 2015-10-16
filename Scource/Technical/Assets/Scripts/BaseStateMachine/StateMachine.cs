﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine{

    private IState stateCurrent;
    private BaseStateType stateType;
    private Dictionary<BaseStateType, IState> dicSystemState;

    public StateMachine()
    {
        dicSystemState = new Dictionary<BaseStateType, IState>();
    }

    public void AddStateAction(BaseStateType _baseStateType, IState _state)
    {
        dicSystemState.Add(_baseStateType, _state);
    }

    public IState GetStateAction(BaseStateType _baseStateType)
    {
        if (dicSystemState.ContainsKey(_baseStateType))
        {
            return dicSystemState[_baseStateType];
        }
#if UNITY_EDITOR
        Debug.LogWarning("Khong ton tai trang thai ku muon lay");
#endif
        return null;
    }

    public void ChangeState(BaseStateType _baseStateType)
    {
        IState state = GetStateAction(_baseStateType);
        if (state != null)
        {
            stateCurrent.EndState();
            stateCurrent = state;
            stateCurrent.BeginState();
        }
        //Neu khong thay doi duoc trang thai thi trang thai cu van hoat dong
    }

    public void MachineStateUpdate()
    {
        if (stateCurrent != null)
        {
            stateCurrent.UpdateState();
        }
    }
}
