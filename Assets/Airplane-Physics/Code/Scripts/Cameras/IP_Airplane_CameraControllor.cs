using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel {
    public class IP_Airplane_CameraControllor : MonoBehaviour
    {
        #region Variables
        [Header("Camera Controller Properties")]
        public IP_BaseAirplane_Input input;
        public int startCameraIndex = 0;
        public List<Camera> cameras = new List<Camera>();

        private int cameraIndex = 0;
        #endregion

        #region BuiltIn Methods
        private void Start()
        {
            if (startCameraIndex >= 0 || startCameraIndex < cameras.Count) {
                DisableAllCameras();
                cameras[startCameraIndex].enabled = true;
                cameras[startCameraIndex].GetComponent<AudioListener>().enabled = true;
            }
        }

        private void Update()
        {
            if (input) {
                if (input.CameraSwitch) {
                    SwitchCamera();
                }
            }
        }
        #endregion

        #region Custom Methods
        protected virtual void SwitchCamera()
        {
            if (cameras.Count > 0) {
                DisableAllCameras();
                cameraIndex++;
                if (cameraIndex >= cameras.Count) {
                    cameraIndex = 0;
                }
                cameras[cameraIndex].enabled = true ;
                cameras[startCameraIndex].GetComponent<AudioListener>().enabled = true;
            }
        }

        void DisableAllCameras() {
            if (cameras.Count > 0) {
                foreach (Camera cam in cameras) {
                    cam.enabled = false;
                    cam.GetComponent<AudioListener>().enabled = false;
                }
            }
        }
        #endregion
    }

}
