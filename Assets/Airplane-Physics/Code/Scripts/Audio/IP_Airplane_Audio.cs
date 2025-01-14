using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel {
    public class IP_Airplane_Audio : MonoBehaviour
    {
        #region Variables
        [Header("Airplane Audio Properties")]
        public IP_BaseAirplane_Input input;
        public AudioSource idleSource;
        public AudioSource fullThrottleAudioSource;
        public float maxPitchValue = 1.2f;

        private float finalVolumeValue;
        private float finalPitchValue;
        #endregion

        #region BuiltIn Methods
        // Start is called before the first frame update
        void Start()
        {
            if (fullThrottleAudioSource) {
                fullThrottleAudioSource.volume = 0f;

            }
        }

        // Update is called once per frame
        void Update()
        {
            if (input) {
                HandleAudio();
            }
        }
        #endregion

        #region Custom Methods
        protected virtual void HandleAudio() {
            finalVolumeValue = Mathf.Lerp(0f, 1f, input.StickyThrottle);
            finalPitchValue = Mathf.Lerp(0f, maxPitchValue, input.StickyThrottle);
            if (fullThrottleAudioSource) {
                fullThrottleAudioSource.volume = finalVolumeValue;
                fullThrottleAudioSource.pitch = finalPitchValue;
            }
        }
        #endregion
    }

}
