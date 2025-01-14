using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel {
    public class IP_Airplane_Propeller : MonoBehaviour
    {
        #region Variables
        [Header("Propeller Properties")]
        public float minRotationRPM = 30f;
        public float minQuadRPMs = 300f;
        public float minTextureSwap = 600f;
        public GameObject mainProp;
        public GameObject blurredProp;

        [Header("Material Properties")]
        public Material BlurredPropMat;
        public Texture2D blurLevel1;
        public Texture2D blurLevel2;
        #endregion

        #region Builtin Methods
        private void Start()
        {
            if (mainProp && blurredProp)
                HandleSwapping(0f);
        }
        #endregion


        #region Custom Methods
        public void HandlePropeller(float currentRPM) {
            float dps = ((currentRPM * 360f) / 60f) * Time.deltaTime;
            dps = Mathf.Clamp(dps, 10f, minRotationRPM);
            transform.Rotate(Vector3.forward, dps);

            if(mainProp && blurredProp)
                HandleSwapping(currentRPM);
        }

        void HandleSwapping(float currentRPM) {
            if (currentRPM > minQuadRPMs)
            {
                blurredProp.gameObject.SetActive(true);
                mainProp.gameObject.SetActive(false);

                if (BlurredPropMat && blurLevel1 && blurLevel2) {

                    if(currentRPM > minTextureSwap)
                        BlurredPropMat.SetTexture("_BaseMap", blurLevel2);
                    else
                        BlurredPropMat.SetTexture("_BaseMap", blurLevel1);
                }
            }
            else {
                blurredProp.gameObject.SetActive(false);
                mainProp.gameObject.SetActive(true);
            }
        }
        #endregion
    }

}
