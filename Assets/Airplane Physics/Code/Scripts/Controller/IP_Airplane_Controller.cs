using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel {
    public class IP_Airplane_Controller : IP_BaseRigidbody_Controller
    {

        #region Variables
        [Header("Base Airplane Properties")]
        public IP_BaseAirplane_Input input;
        public Transform centerOfGravity;

        [Tooltip("Weight is in LBS")]
        public float airplaneWeight = 800f;
        #endregion

        #region Custom Methods
        protected override void HandlePhysics()
        {
            HandleEngines();
            HandleAeroDynamics();
            HandleSteerings();
            HandleBrakes();
            HandleAltitude();
        }

        void HandleEngines() { }
        void HandleAeroDynamics() { }
        void HandleSteerings() { }
        void HandleBrakes() { }
        void HandleAltitude() { }
        #endregion
    }
}

