using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBool : StateMachineBehaviour {

    public string boolName;
    public bool status;

    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool (boolName, status);
    } // Override OnStateUpdate

    public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool (boolName, !status);
    } // Override OnStateExit

} // Namespace KeepBool
