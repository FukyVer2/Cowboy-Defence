using UnityEngine;
using System.Collections;

public interface IState{

    void BeginState();
    void UpdateState();
    void EndState();
}
