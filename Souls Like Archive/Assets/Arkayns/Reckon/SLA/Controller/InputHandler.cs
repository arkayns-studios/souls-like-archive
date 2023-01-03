using UnityEngine;

namespace Arkayns.Reckon.SLA {

    public class InputHandler : MonoBehaviour {

        // -- Variables --
        private float m_vertical;
        private float m_horizontal;
        private bool m_runInput;

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
            m_runInput = Input.GetButton ("RunInput");
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

            if (m_runInput) m_states.run = (m_states.moveAmount > 0);
            else m_states.run = false;
        } // UpdateStates ()

    } // Class InputHandler

} // Namespace Arkayns Reckon SLA