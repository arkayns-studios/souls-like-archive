using UnityEngine;

namespace Arkayns.Reckon.SLA {

    public class StateManager : MonoBehaviour {
        
        // -- Variables --
        [Header ("Init")]
        public GameObject activeModel;

        [Header ("Inputs")]
        [Range (-1, 1)] public float vertical;
        [Range (-1, 1)] public float horizontal;
        public float moveAmount;
        public Vector3 moveDir;

        [Header ("Stats")]
        public float moveSpeed = 2;
        public float runSpeed = 3.5f;
        public float rotateSpeed = 5;
        public float toGround = 0.5f;

        [Header ("States")]
        public bool onGround;
        public bool run;
        public bool lockOn;

        [HideInInspector] public Animator anim; 
        [HideInInspector] public Rigidbody rigid;

        [HideInInspector] public float delta;
        [HideInInspector] public LayerMask ignoreLayers;

        // -- Methods --
        public void Init () {
            SetupAnimator ();
            rigid = GetComponent<Rigidbody> ();
            rigid.angularDrag = 999;
            rigid.drag = 4;
            rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);
        } // Init ()

        private void SetupAnimator () {
            if (activeModel == null) {
                anim = GetComponentInChildren<Animator> ();
                activeModel = anim.gameObject;
                if (anim == null) Debug.Log ("No model found");
            } else activeModel = anim.gameObject;

            if (anim == null) anim = activeModel.GetComponent<Animator> ();
        } // SetupAnimator ()

        public void FixedTick (float d) {
            delta = d;

            rigid.drag = (moveAmount > 0 || onGround == false) ? 0 : 4;

            var targetSpeed = moveSpeed;
            if (run) targetSpeed = runSpeed;
            if (onGround) rigid.velocity = moveDir * (targetSpeed * moveAmount);
            if (run) lockOn = false;

            if (!lockOn) {
                var targetDir = moveDir;
                targetDir.y = 0;
                if (targetDir == Vector3.zero) targetDir = transform.forward;
                var targetRot = Quaternion.LookRotation (targetDir);
                var targetRotation = Quaternion.Slerp (transform.rotation, targetRot, delta * moveAmount * rotateSpeed);
                transform.rotation = targetRotation;
            }

            HandleMovementAnimations ();
        } // FixedTick ()

        public void Tick (float d) {
            delta = d;
            onGround = OnGround ();
            anim.SetBool ("onGround", onGround);
        } // Tick ()

        private void HandleMovementAnimations () {
            anim.SetBool ("run", run);
            anim.SetFloat ("vertical", moveAmount, 0.4f, delta);
        } // HandleMovementAnimations ()

        public bool OnGround () {
            var r = false;

            var origin = transform.position + (Vector3.up * toGround);
            var dir = -Vector3.up;
            var dis = toGround + 0.3f;

            Debug.DrawRay (origin, dir * dis);
            if (Physics.Raycast(origin, dir, out var hit, dis, ignoreLayers)) {
                r = true;
                var targetPosition = hit.point;
                transform.position = targetPosition;
            }

            return r;
        } // OnGround ()

    } // Class StateManager

} // Namespace Arkayns Reckon SLA