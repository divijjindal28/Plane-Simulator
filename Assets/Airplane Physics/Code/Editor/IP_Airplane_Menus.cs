using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace IndiePixel {
    public static class IP_Airplane_Menus
    {
        [MenuItem("Airplane Tools/Create New Airplane")]

        public static void CreateNewAirplane() {
            GameObject curSelected = Selection.activeGameObject;
            if (curSelected) {
                IP_Airplane_Controller curController =  curSelected.AddComponent<IP_Airplane_Controller>();
                GameObject curCOG = new GameObject("COG");
                curCOG.transform.SetParent(curSelected.transform);
                curController.centerOfGravity = curCOG.transform;
            }

        }
    }
}

