using UnityEngine;

namespace Arkayns.Reckon.SLA {

    public class CameraManager : MonoBehaviour {

        // -- Variables --
        public static CameraManager singleton;

        public bool lockOn;
        public float followSpeed = 9;
        public float mouseSpeed = 2;
        public float controllerSpeed = 5;

        public Transform target;

        [HideInInspector] public Transform pivot;
        [HideInInspector] public Transform camTrans;

        private float m_turnSmoothing = .1f;
        public float minAngle = -35;
        public float maxAngle = 35;

        private float smoothX;
        private float smoothXVelocity;
        private float smoothY;
        private float smoothYVelocity;
        [SerializeField] private float lookAngle;
        [SerializeField] private float tiltAngle;

        // -- Built-In Methods --
        private void Awake () {
            singleton = this;
        } // Awake ()

        // -- Methods --
        public void Init (Transform t) {
            target = t;

            if (Camera.main != null) camTrans = Camera.main.transform;
            pivot = camTrans.parent;
        } // Init ()

        public void Tick (float d) {
            var h = Input.GetAxis ("Mouse X");
            var v = Input.GetAxis ("Mouse Y");

            var cH = Input.GetAxis ("RightAxis X");
            var cV = Input.GetAxis ("RightAxis Y");

            var targetSpeed = mouseSpeed;

            if (cV != 0 || cH != 0) {
                h = cH;
                v = cV;
                targetSpeed = controllerSpeed;
            }

            HandlePosition (d);
            HandleRotation (d, v, h, targetSpeed);
        } // FixedTick ()

        private void HandleRotation (float d, float v, float h, float targetSpeed) {
            if (m_turnSmoothing > 0) {
                smoothX = Mathf.SmoothDamp (smoothX, h, ref smoothXVelocity, m_turnSmoothing);
                smoothY = Mathf.SmoothDamp (smoothY, v, ref smoothYVelocity, m_turnSmoothing);
            } else {
                smoothX = h;
                smoothY = v;
            }

            if (lockOn) {
                Debug.Log ("Not implemented yet!");
            }

            lookAngle += smoothX * targetSpeed;
            transform.rotation = Quaternion.Euler (0, lookAngle, 0);

            tiltAngle -= smoothY * targetSpeed;
            tiltAngle = Mathf.Clamp (tiltAngle, minAngle, maxAngle);
            pivot.localRotation = Quaternion.Euler (tiltAngle, 0, 0);
        } // HandleRotation ()

        private void HandlePosition (float d) {
            var speed = d * followSpeed;
            var targetPosition = Vector3.Lerp (transform.position, target.position, speed);
            transform.position = targetPosition;
        } // HandlePosition ()

    } // Class CameraManager

} // Namespace Arkayns Reckon SLA