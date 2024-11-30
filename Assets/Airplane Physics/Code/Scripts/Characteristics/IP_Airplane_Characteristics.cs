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
        public float lerpSpeed;
        private Rigidbody rb;
        private float startDrag;
        private float startAngularDrag ;
        private float angleOfAttack;
        private float pitchAngle;
        private float rollAngle;
        private IP_BaseAirplane_Input input;
        [Header("Lift Properties")]
        public float maxLiftPower = 800f;
        public AnimationCurve liftcurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private float maxMPS;
        public float normalisedMPH;

        public Vector3 finalLiftForce2;
        public Vector3 liftDir2;

        [Header("Drag Properties")]
        public float dragFactor = 0.01f;
        public float flapDragFactor = 0.005f;

        [Header("Control Properties")]
        public float pitchSpeed = 10f;
        public float rollSpeed = 10f;
        public float yawSpeed = 100f;
        public Vector3 rollTorque2;
        #endregion


        #region Constants
        const float mpsTomph = 2.23694f;
        #endregion


        #region Builtin Methods
        #endregion


        #region Custom Methods
        public void InitCharacteristics(Rigidbody curRB, IP_BaseAirplane_Input curInput) {
            input = curInput;
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
                HandlePitch();
                HandleRigidbodyTransform();
                HandleRoll();
                HandleYaw();
                HandleBanking();
            }
            
        }

        void CalculateForwardSpeed() {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            speed = rb.velocity.magnitude;
            forwardSpeed = Mathf.Max(0, localVelocity.z);
            forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, maxMPS);
            
            mph = forwardSpeed * mpsTomph;
            mph = Mathf.Clamp(mph, 0f, maxMPH);
            normalisedMPH = Mathf.InverseLerp(0f, maxMPH , mph);
            //Debug.DrawRay(transform.position,transform.position+rb.velocity, Color.green);
        }

        void CalculateLift() {
            angleOfAttack = Vector3.Dot(rb.velocity.normalized,transform.forward);
            angleOfAttack *= angleOfAttack;


            Vector3 liftDir = transform.up;
            liftDir2 = liftDir;
            float liftPower = liftcurve.Evaluate(normalisedMPH) * maxLiftPower;
            Vector3 finalLiftForce = liftDir * liftPower *angleOfAttack;
            finalLiftForce2 = finalLiftForce;
            rb.AddForce(finalLiftForce);

        }

        void CalculateDrag() {
            float speedDrag = forwardSpeed * dragFactor;
            float flapDrag = input.Flaps * flapDragFactor;
            float finalDrag = startDrag + speedDrag + flapDrag;

            rb.drag = finalDrag;
            rb.angularDrag = startAngularDrag * forwardSpeed;
        }

        void HandleRigidbodyTransform() {
            if (rb.velocity.magnitude > 1f) {
                Vector3 updatedVelocity = Vector3.Lerp(rb.velocity,
                    transform.forward * forwardSpeed, forwardSpeed * angleOfAttack * Time.deltaTime * lerpSpeed);
                rb.velocity = updatedVelocity;


                Quaternion updatedRotation = Quaternion.Slerp(
                    rb.rotation, Quaternion.LookRotation(rb.velocity,
                    transform.up), Time.deltaTime * lerpSpeed);
                rb.MoveRotation(updatedRotation);
            }

        }

        void HandlePitch() {
            Vector3 flatForward = transform.forward;
            flatForward.y = 0;
            flatForward = flatForward.normalized;
            pitchAngle = Vector3.Angle(transform.forward, flatForward);
            Vector3 pitchTorque = input.Pitch * pitchSpeed * transform.right;
            rb.AddTorque(pitchTorque);
        }

        void HandleRoll() {
            Vector3 flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;
            rollAngle = Vector3.SignedAngle(transform.right, flatRight,transform.forward);

            Vector3 rollTorque = -input.Roll * rollSpeed * transform.forward;
            rollTorque2 = rollTorque;
            rb.AddTorque(rollTorque);
        }

        void HandleYaw() {
            Vector3 YawTorque = input.Yaw * yawSpeed * transform.up;
            rb.AddTorque(YawTorque);
        }


        void HandleBanking() {
            float bankSide = Mathf.InverseLerp(-90f, 90f, rollAngle);
            float bankAmount = Mathf.Lerp(-1, 1, bankSide);

            Vector3 bankTorque = bankAmount * rollSpeed * transform.up;
            rb.AddTorque(bankTorque);
        }

        #endregion
    }
}
