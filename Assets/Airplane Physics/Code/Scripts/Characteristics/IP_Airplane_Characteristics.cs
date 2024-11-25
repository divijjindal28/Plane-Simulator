using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IndiePixel {
    public class IP_Airplane_Characteristics : MonoBehaviour
    {
        #region Variables
        [Header("Characteristics Properties")]
        public float speed;
        public float forwardSpeed;
        public float mph;
        public float maxMPH = 110f;

        private Rigidbody rb;
        private float startDrag;
        private float startAngularDrag ;

        [Header("Lift Properties")]
        public float maxLiftPower = 800f;
        public AnimationCurve liftcurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private float maxMPS;
        public float normalisedMPH;

        public Vector3 finalLiftForce2;
        public Vector3 liftDir2;
        #endregion


        #region Constants
        const float mpsTomph = 2.23694f;
        #endregion


        #region Builtin Methods
        #endregion


        #region Custom Methods
        public void InitCharacteristics(Rigidbody curRB) {
            rb = curRB;
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;

            maxMPS = maxMPH / mpsTomph;
        }

        public void UpdateCharacteristics() {
            if (rb) {
                CalculateForwardSpeed();
                CalculateLift();
                CalculateDrag();
            }
            
        }

        void CalculateForwardSpeed() {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            speed = rb.velocity.magnitude;
            forwardSpeed = Mathf.Max(0, localVelocity.z);
            //forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, maxMPS);
            
            mph = forwardSpeed * mpsTomph;
            mph = Mathf.Clamp(mph, 0f, maxMPH);
            normalisedMPH = Mathf.InverseLerp(0f, maxMPH , mph);
            Debug.DrawRay(transform.position,transform.position+localVelocity, Color.green);
        }

        void CalculateLift() {
            Vector3 liftDir = transform.up;
            liftDir2 = liftDir;
            float liftPower = liftcurve.Evaluate(normalisedMPH) * maxLiftPower;
            Vector3 finalLiftForce = liftDir * liftPower;
            finalLiftForce2 = finalLiftForce;
            rb.AddForce(finalLiftForce);

        }

        void CalculateDrag() {

        }
        
        #endregion
    }
}
