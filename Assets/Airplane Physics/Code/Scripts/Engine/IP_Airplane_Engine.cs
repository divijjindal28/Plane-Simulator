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

        #endregion

        #region Builtin Methods
        #endregion


        #region Custom Methods
        public Vector3 CalculateForce(float throttle) {
            float finalThrottle = Mathf.Clamp01(throttle);
            finalThrottle = powerCurve.Evaluate(finalThrottle);

            float currentRPM = finalThrottle * maxRPM;
            if (propeller) {
                propeller.HandlePropeller(currentRPM);
            }

            float finalPower = finalThrottle * maxForce;
            Vector3 finalForce = transform.TransformDirection(transform.forward) * finalPower;
            return finalForce;
        }
        #endregion
    }
}

