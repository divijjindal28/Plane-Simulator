using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel {
    [RequireComponent(typeof(IP_Airplane_Characteristics))]
    public class IP_Airplane_Controller : IP_BaseRigidbody_Controller
    {

        #region Variables
        [Header("Base Airplane Properties")]
        public IP_BaseAirplane_Input input;
        public IP_Airplane_Characteristics characteristics;
        public Transform centerOfGravity;

        [Tooltip("Weight is in LBS")]
        public float airplaneWeight = 800f;

        [Header("Engines")]
        public List<IP_Airplane_Engine> engines = new List<IP_Airplane_Engine>();

        [Header("Wheels")]
        public List<IP_Airplane_Wheel> wheels = new List<IP_Airplane_Wheel>();

        [Header("Control Surfaces")]
        public List<IP_Airplane_ControlSurfaces> controlSurfaces = new List<IP_Airplane_ControlSurfaces>();
        #endregion

        #region Constants
        const float poundsToKilos = 0.453592f;
        #endregion

        #region Builtin Methods
        public override void Start()
        {
            base.Start();

            float finalMass = airplaneWeight * poundsToKilos;
            if (rb) {
                rb.mass = finalMass;
                if (centerOfGravity) {
                    rb.centerOfMass = centerOfGravity.localPosition;
                }
                characteristics = GetComponent<IP_Airplane_Characteristics>();
                if (characteristics)
                {
                    characteristics.InitCharacteristics(rb, input);
                }
            }

            if (wheels != null) {
                if (wheels.Count > 0) {
                    foreach (IP_Airplane_Wheel wheel in wheels) {
                        wheel.InitWheel();
                    }
                }
            }

            
        }
        #endregion

        #region Custom Methods
        protected override void HandlePhysics()
        {
            if (input) {
                HandleEngines();
                HandleCharacteristics();
                HandleWheel();
                HandleAltitude();
                HandleControlSurfaces();
            }
            
        }

        void HandleEngines() {
            if (engines != null) {
                if (engines.Count > 0) {
                    foreach (IP_Airplane_Engine engine in engines)
                    {
                        rb.AddForce(engine.CalculateForce(input.StickyThrottle));
                    }
                }
            }
        }
        void HandleCharacteristics() {
            if (characteristics)
            {
                characteristics.UpdateCharacteristics();
            }
        }

        void HandleControlSurfaces()
        {
            if (controlSurfaces.Count > 0)
            {
                foreach (IP_Airplane_ControlSurfaces controlSurface in controlSurfaces)
                {
                    controlSurface.HandleControlSurface(input);
                }
            }
        }


        void HandleWheel() {
            if (wheels != null)
            {
                if (wheels.Count > 0)
                {
                    foreach (IP_Airplane_Wheel wheel in wheels)
                    {
                        wheel.HandleWheel(input);
                    }
                }
            }
        }
        void HandleAltitude() { }
        #endregion
    }
}

