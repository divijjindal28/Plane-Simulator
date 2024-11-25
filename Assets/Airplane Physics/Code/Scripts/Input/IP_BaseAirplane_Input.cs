using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel {
    public class IP_BaseAirplane_Input : MonoBehaviour
    {
        #region Variables
        public float throttleSpeed = 0.3f;
        protected float stickyThrottle;
        
        protected float pitch = 0f;
        protected float roll = 0f;
        protected float yaw = 0f;
        protected float throttle = 0f;
        protected int flaps = 0;
        public int maxFlapsIncrements = 3;
        public KeyCode brakeKey = KeyCode.Space;
        protected float brake = 0;
        #endregion

        #region Properties
        public float Pitch { get { return pitch; } }
        public float Roll { get { return roll; } }
        public float Yaw { get { return yaw; } }
        public float Throttle { get { return throttle; } }
        public float Flaps { get { return flaps; } }
        public float Brake { get { return brake; } }
        public float StickyThrottle
        {
            get { return stickyThrottle; }
        }
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
        }
        #endregion

        #region Custom Methods
        protected virtual void HandleInput() {
            pitch = Input.GetAxis("Vertical");
            roll = Input.GetAxis("Horizontal");
            yaw = Input.GetAxis("Yaw");
            throttle = Input.GetAxis("Throttle");
            StickyThrottleControl();

            brake = Input.GetKey(brakeKey) ? 1f: 0f;

            if (Input.GetKeyDown(KeyCode.F)) {
                flaps += 1;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                flaps -= 1;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlapsIncrements);
        }

        void StickyThrottleControl()
        {
            stickyThrottle = stickyThrottle + (-throttle * throttleSpeed * Time.deltaTime);
            stickyThrottle = Mathf.Clamp01(stickyThrottle);

        }
        #endregion
    }
}

