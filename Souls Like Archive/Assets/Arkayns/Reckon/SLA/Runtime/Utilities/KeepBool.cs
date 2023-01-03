using UnityEngine;

namespace Arkayns.Reckon.SLA {

    public class KeepBool : StateMachineBehaviour {

        // -- Variables --
        public string boolName;
        public bool status;
        
        // -- Built-In Methods --
        public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.SetBool (boolName, status);
        } // Override OnStateUpdate ()

        public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.SetBool (boolName, !status);
        } // Override OnStateExit ()

    } // Class KeepBool

} // Namespace KeepBool
