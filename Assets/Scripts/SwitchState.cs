using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchState : MonoBehaviour
{
    public NodeState myNodeState;
    private bool isCleared = false;
    public SupplyManagement supplyManagement;

    public void SwitchMaterials() {
        isCleared = !isCleared; // flip the bool when this function is triggered
        myNodeState.flipNodeState();
        StartCoroutine("checkIfEnergyIsNegative");
    }

    // check when the energy is negative when switch is triggered
    // switch back the state when energy is negative after 1s
    private IEnumerator checkIfEnergyIsNegative() {
        yield return new WaitForSeconds(1f);
        
        if (supplyManagement.getCurrentEnergyAvailable() < 0) {
            isCleared = !isCleared; // flip the bool when this function is triggered
            myNodeState.flipNodeState();
        }
    } 
}
