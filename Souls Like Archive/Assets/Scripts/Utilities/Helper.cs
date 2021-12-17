using UnityEngine;

namespace Arkayns {

    public class Helper : MonoBehaviour {


        [Range (-1, 1)] public float vertical;
        [Range (-1, 1)] public float horizontal;
        [Space]
        public bool playAnim;
        public bool useItem;
        public bool lockon;
        [Space]
        public string [] oh_attacks;
        public string [] th_attacks;
        [Space]
        public bool twoHanded;
        public bool enableRM;
        public bool interacting;

        private Animator anim;
        
        private void Start () {
            anim = GetComponent<Animator> ();
        } // Start

        private void Update () {
            enableRM = !anim.GetBool ("canMove");
            anim.applyRootMotion = enableRM;

            interacting = anim.GetBool ("interacting");

            if (!lockon) {
                horizontal = 0;
                vertical = Mathf.Clamp01 (vertical);
            }

            anim.SetBool ("lockon", lockon);

            if (enableRM)
                return;

            if (useItem) {
                anim.Play ("use_item");
                useItem = false;
            }

            if (interacting) {
                playAnim = false;
                vertical = Mathf.Clamp (vertical, 0, 0.5f);
            }

            anim.SetBool ("two_handed", twoHanded);

            if (playAnim) {
                string targetAnim;

                if (twoHanded) {
                    int r = Random.Range (0, th_attacks.Length);
                    targetAnim = th_attacks [r];
                } else {
                    int r = Random.Range (0, oh_attacks.Length);
                    targetAnim = oh_attacks [r];

                    if (vertical > 0.5f)
                        targetAnim = "oh_attack_3";
                }

                vertical = 0;
                anim.CrossFade (targetAnim, 0.2f);
                playAnim = false;
            }

            anim.SetFloat ("vertical", vertical);
            anim.SetFloat ("horizontal", horizontal);

        } // Update

    } // Class Helper

} // Namespace Arkayns