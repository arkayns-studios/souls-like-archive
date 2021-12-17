using UnityEngine;

namespace Arkayns {

    public class InputHandler : MonoBehaviour {

        private float vertical;
        private float horizontal;
        private bool runInput;

        private StateManager states;
        private CameraManager camManager;

        private float delta;

        private void Start () {
            states = GetComponent<StateManager> ();
            states.Init ();

            camManager = CameraManager.singleton;
            camManager.Init (this.transform);
        } // Start

        private void Update () {
            delta = Time.deltaTime;
            states.Tick (delta);
        } // Update

        private void FixedUpdate () {
            delta = Time.fixedDeltaTime;
            GetInput ();
            UpdateStates ();
            states.FixedTick (delta); // This can be used for running controller indepent from frame rate
            camManager.Tick (delta);
        } // FixedUpdate

        private void GetInput () {
            vertical = Input.GetAxis ("Vertical");
            horizontal = Input.GetAxis ("Horizontal");

            runInput = Input.GetButton ("RunInput");
        } // GetInput

        private void UpdateStates () {
            states.vertical = vertical;
            states.horizontal = horizontal;

            Vector3 v = vertical * camManager.transform.forward;
            Vector3 h = horizontal * camManager.transform.right;
            states.moveDir = (v + h).normalized;
            float m = Mathf.Abs (horizontal) + Mathf.Abs (vertical);
            states.moveAmount = Mathf.Clamp01 (m);

            if (runInput)
                states.run = (states.moveAmount > 0);
            else
                states.run = false;

        } // UpdateStates

    } // Class InputHandler

} // Namespace Arkayns