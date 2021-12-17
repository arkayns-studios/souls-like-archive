using UnityEngine;

namespace Arkayns {

    public class CameraManager : MonoBehaviour {

        public static CameraManager singleton;

        public bool lockOn;
        public float followSpeed = 9;
        public float mouseSpeed = 2;
        public float controllerSpeed = 5;

        public Transform target;

        [HideInInspector]
        public Transform pivot;
        [HideInInspector]
        public Transform camTrans;

        private float turnSmoothing = .1f;
        public float minAngle = -35;
        public float maxAngle = 35;

        private float smoothX;
        private float smoothXVelocity;
        private float smoothY;
        private float smoothYVelocity;
        [SerializeField] private float lookAngle;
        [SerializeField] private float tiltAngle;

        private void Awake () {
            singleton = this;
        } // Awake

        public void Init (Transform t) {
            target = t;

            camTrans = Camera.main.transform;
            pivot = camTrans.parent;
        } // Init

        public void Tick (float d) {
            float h = Input.GetAxis ("Mouse X");
            float v = Input.GetAxis ("Mouse Y");

            float c_h = Input.GetAxis ("RightAxis X");
            float c_v = Input.GetAxis ("RightAxis Y");

            float targetSpeed = mouseSpeed;

            if (c_v != 0 || c_h != 0) {
                h = c_h;
                v = c_v;
                targetSpeed = controllerSpeed;
            }

            HandlePosition (d);
            HandleRotation (d, v, h, targetSpeed);
        } // FixedTick

        private void HandleRotation (float d, float v, float h, float targetSpeed) {
            if (turnSmoothing > 0) {
                smoothX = Mathf.SmoothDamp (smoothX, h, ref smoothXVelocity, turnSmoothing);
                smoothY = Mathf.SmoothDamp (smoothY, v, ref smoothYVelocity, turnSmoothing);
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
        } // HandleRotation

        private void HandlePosition (float d) {
            float speed = d * followSpeed;
            Vector3 targetPosition = Vector3.Lerp (transform.position, target.position, speed);
            transform.position = targetPosition;
        } // HandlePosition

    } // Class CameraManager

} // Namespace Arkayns