using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            if (instruments.Count > 0) {
                Debug.Log("IP_Airplane_Tachometer HandleAirplaneUI RPM : " + instruments.Count);
                foreach (IAirplaneUI instrument in instruments) {
                    instrument.HandleAirplaneUI();
                }
            }
        }

        #endregion

        
    }

}
