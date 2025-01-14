using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel {
    public class IP_Airplane_Engine : MonoBehaviour
    {
        #region Variables
        [Header("Engine Properties")]
        public float maxForce = 5000f;
        public float maxRPM = 2550;


        public AnimationCurve powerCurve = AnimationCurve.Linear(
            0f,0f,1f,1f);

        [Header("Propellers")]
        public IP_Airplane_Propeller propeller;

        private float TEST__finalPower;
        private float TEST__finalThrottle;
        #endregion

        #region Builtin Methods
        private void Update()
        {
            //Debug.Log("FinalForce : "+ TEST__finalThrottle);
        }
        #endregion


        #region Custom Methods
        public Vector3 CalculateForce(float throttle) {
            float finalThrottle = Mathf.Clamp01(throttle);
            finalThrottle = powerCurve.Evaluate(finalThrottle);
            TEST__finalThrottle = finalThrottle;
            float currentRPM = finalThrottle * maxRPM;
            if (propeller) {
                propeller.HandlePropeller(currentRPM);
            }

            float finalPower = finalThrottle * maxForce;
            TEST__finalPower = finalPower;
            Vector3 finalForce = transform.forward * finalPower;
            return finalForce;
        }
        #endregion
    }
}

