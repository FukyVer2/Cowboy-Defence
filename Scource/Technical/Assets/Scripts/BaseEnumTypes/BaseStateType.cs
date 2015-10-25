using UnityEngine;
using System.Collections;

public enum BaseStateType
{
    //Enemy State Type
    ES_IDLE = 0,
    ES_ATTACK = 1,
    ES_RUN = 2,
    ES_DIE = 3,
    ES_JUMP = 4,
    

    //Player State Type
    PS_IDLE = 5,
    PS_ATTACK = 6,
    PS_DIE = 7,

    //None state
    ES_NONE = 8,
    // Boss state
    BS_IDLE = 10,
    BS_ATTACK = 11,
    BS_SUMMON = 12,
    BS_DIE = 13,
    BS_RUN =14
}

