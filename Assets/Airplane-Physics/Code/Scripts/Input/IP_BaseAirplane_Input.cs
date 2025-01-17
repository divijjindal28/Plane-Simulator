using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        [SerializeField]
        protected KeyCode cameraKey = KeyCode.C;
        protected bool cameraSwitch = false;
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
        public bool CameraSwitch { get { return cameraSwitch; } }

        public TextMeshPro textMeshProUGUI;
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
            StickyThrottleControl();
            ClampInputs();
        }
        #endregion

        #region Custom Methods

        public void OnRightJoystickValueChangeX(float x)
        {
            Debug.Log("IP_BaseAirplane_Input : OnRightJoystickValueChangeX : " + x.ToString());
            yaw = x;
        }
        
        public void OnRightJoystickValueChangeY(float y)
        {
            Debug.Log("IP_BaseAirplane_Input : OnRightJoystickValueChangeY : " + y.ToString());
            throttle = -y;
        }

        public void OnLeftJoystickValueChangeX(float x)
        {
            Debug.Log("IP_BaseAirplane_Input : OnLeftJoystickValueChangeX : " + x.ToString());
            roll = x;
        }

        public void OnLeftJoystickValueChangeY(float y)
        {
            Debug.Log("IP_BaseAirplane_Input : OnLeftJoystickValueChangeY : " + y.ToString()); ;
            pitch = y;
        }

        public void OnBrakesApplied()
        {
            brake = 1f;
        }

        public void OnBrakeReleased()
        {
            brake = 0f;
        }

        public void OnFlapValueChange(float y)
        {
            if (y >= 0 && y < 0.25f) {
                flaps = 0;
            }
            else if(y >= 0.25f && y < 0.5f) {
                flaps = 1;
            }
            else if (y >= 0.5f && y < 0.75f)
            {
                flaps = 2;
            }
            else if (y >= 0.75f && y <= 1f)
            {
                flaps = 3;
            }
        }

        protected virtual void HandleInput() {
            //pitch = Input.GetAxis("Vertical");
            //roll = Input.GetAxis("Horizontal");
            //yaw = Input.GetAxis("Yaw");
            //throttle = Input.GetAxis("Throttle");

            textMeshProUGUI.text = brake.ToString();
            brake = Input.GetKey(brakeKey) ? 1f: 0f;

            if (Input.GetKeyDown(KeyCode.F)) {
                flaps += 1;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                flaps -= 1;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlapsIncrements);

            cameraSwitch = Input.GetKeyDown(cameraKey);
        }

        void StickyThrottleControl()
        {
            stickyThrottle = stickyThrottle + (-throttle * throttleSpeed * Time.deltaTime);
            stickyThrottle = Mathf.Clamp01(stickyThrottle);

        }

        protected void ClampInputs()
        {
            pitch = Mathf.Clamp(pitch, -1, 1);
            roll = Mathf.Clamp(roll, -1, 1);
            yaw = Mathf.Clamp(yaw, -1, 1);
            throttle = Mathf.Clamp(throttle, -1, 1);
        }
        #endregion
    }
}

