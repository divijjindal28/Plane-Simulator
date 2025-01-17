using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace IndiePixel {
    public class IP_AirplaneUI_Controller : MonoBehaviour
    {
        #region Variables
        public List<IAirplaneUI> instruments = new List<IAirplaneUI>();
        #endregion

        #region Builtin Methods

        // Start is called before the first frame update
        void Start()
        {
            instruments = transform.GetComponentsInChildren<IAirplaneUI>().ToList<IAirplaneUI>();
        }
        private void Update()
        {
            int i = 0;

            if (instruments.Count > 0) {
                //Debug.Log("IP_Airplane_Tachometer HandleAirplaneUI RPM : " + instruments.Count);
                foreach (IAirplaneUI instrument in instruments) {
                    try
                    {
                        instrument.HandleAirplaneUI();
                    }
                    catch (Exception e) {
                        Debug.Log("IP_Airplane_Tachometer HandleAirplaneUI RPM num err : " + e.ToString());
                    }
                    
                    Debug.Log("IP_Airplane_Tachometer HandleAirplaneUI RPM num : " + i.ToString());
                    i += 1;
                    Debug.Log("IP_Airplane_Tachometer HandleAirplaneUI RPM num : " + i.ToString());
                }
            }
        }

        #endregion

        
    }

}
