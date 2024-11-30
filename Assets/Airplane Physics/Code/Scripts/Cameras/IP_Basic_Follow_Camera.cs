using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel
{
    public class IP_Basic_Follow_Camera : MonoBehaviour
    {

        #region Variables
        [Header("Basic Follow Camera Properties")]
        public Transform Target;
        public float distance = 5f;
        public float height = 2f;
        public float smoothSpeed = 0.5f;

        private Vector3 smoothVelocity;
        protected float origHeight;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            origHeight = height;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Target) {
                HandleCamera();
            }
            
        }
        #endregion


        #region Custom Methods
        protected virtual void HandleCamera()
        {
            Vector3 wantedPosition = Target.position + (-Target.forward * distance) +
                (Vector3.up * height);
            Debug.DrawLine(Target.position, wantedPosition, Color.blue); ;
            transform.position = Vector3.SmoothDamp(transform.position, wantedPosition,
                ref smoothVelocity, smoothSpeed);

            transform.LookAt(Target);
        }
        #endregion

    }

}
