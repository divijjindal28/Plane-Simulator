using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel {
    [RequireComponent(typeof(WheelCollider))]
    public class IP_Airplane_Wheel : MonoBehaviour
    {
        #region Variables
        private WheelCollider WheelCol;
        #endregion

        #region Builtin Methods
        private void Start()
        {
            WheelCol = GetComponent<WheelCollider>();
        }
        #endregion


        #region Custom Methods
        public void InitWheel() {
            if (WheelCol) {
                WheelCol.motorTorque = 0.0000000000000001f;
            }
        }
        #endregion
    }
}


