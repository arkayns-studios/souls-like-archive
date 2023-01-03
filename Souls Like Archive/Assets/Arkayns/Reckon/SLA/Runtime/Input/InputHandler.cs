using UnityEngine;

namespace Arkayns.Reckon.SLA {

    public class InputHandler : MonoBehaviour {

        // -- Variables --
        private float m_vertical, m_horizontal;
        private bool m_bInput, m_aInput, m_xInput, m_yInput;
        private bool m_rbInput, m_lbInput, m_rtInput, m_ltInput;
        private float m_rtAxis, m_ltAxis;

        private StateManager m_states;
        private CameraManager m_camManager;
        private float m_delta;

        // -- Built-In Methods --
        private void Start () {
            m_states = GetComponent<StateManager> ();
            m_states.Init ();

            m_camManager = CameraManager.singleton;
            m_camManager.Init (this.transform);
        } // Start ()

        private void Update () {
            m_delta = Time.deltaTime;
            m_states.Tick (m_delta);
        } // Update ()

        private void FixedUpdate () {
            m_delta = Time.fixedDeltaTime;
            GetInput ();
            UpdateStates ();
            m_states.FixedTick (m_delta); // This can be used for running controller independent from frame rate
            m_camManager.Tick (m_delta);
        } // FixedUpdate ()

        // -- Methods --
        private void GetInput () {
            m_vertical = Input.GetAxis ("Vertical");
            m_horizontal = Input.GetAxis ("Horizontal");
            
            m_aInput = Input.GetButton ("AButton");
            m_bInput = Input.GetButton ("BButton");
            m_xInput = Input.GetButton ("XButton");
            m_yInput = Input.GetButtonUp ("YButton");
            
            m_rtInput = Input.GetButton ("RT");
            m_rtAxis = Input.GetAxis ("RT");
            if (m_rtAxis != 0) m_rtInput = true;
            
            m_ltInput = Input.GetButton ("LT");
            m_ltAxis = Input.GetAxis ("LT");
            if (m_ltAxis != 0) m_ltInput = true;
            
            m_rbInput = Input.GetButton ("RB");
            m_lbInput = Input.GetButton ("LB");

        } // GetInput ()

        private void UpdateStates () {
            m_states.vertical = m_vertical;
            m_states.horizontal = m_horizontal;

            var managerTransform = m_camManager.transform;
            var v = m_vertical * managerTransform.forward;
            var h = m_horizontal * managerTransform.right;
            m_states.moveDir = (v + h).normalized;
            var m = Mathf.Abs (m_horizontal) + Mathf.Abs (m_vertical);
            m_states.moveAmount = Mathf.Clamp01 (m);

            if (m_bInput) m_states.run = (m_states.moveAmount > 0);
            else m_states.run = false;

            m_states.rtInput = m_rtInput;
            m_states.ltInput = m_ltInput;
            m_states.rbInput = m_rbInput;
            m_states.lbInput = m_lbInput;

            if (m_yInput) {
                m_states.isTwoHanded = !m_states.isTwoHanded;
                m_states.HandleTwoHanded();
            }
                
        } // UpdateStates ()

    } // Class InputHandler

} // Namespace Arkayns Reckon SLA