using System;
using UnityEngine;

namespace Arkayns.Reckon.SLA {

    public class AnimatorHook : MonoBehaviour {

        // -- Variables --
        private Animator m_anim;
        private StateManager m_state;
        
        // -- Built-In Methods --
        private void OnAnimatorMove() {
            if (m_state.canMove) return;

            //m_state.rigid.drag = 0;
            var multiplier = 1f;
            var delta = m_anim.deltaPosition;
            delta.y = 0;
            var velocity = (delta * multiplier) / m_state.delta;
            m_state.rigid.velocity = velocity;
        } // OnAnimatorMove ()

        // -- Methods --
        public void Init(StateManager state) {
            m_state = state;
            m_anim = state.anim;
        } // Init ()

        public void LateTick() {
            
        } // LateTick ()
        
    } // Class AnimatorHook

} // Namespace Arkayns Reckon SLA