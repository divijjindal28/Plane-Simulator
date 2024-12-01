using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndiePixel
{
    [CustomEditor(typeof(IP_XboxAirplane_Input))]
    public class IP_XboxAirPlaneInput_Editor : Editor
    {
        #region Variables
        private IP_XboxAirplane_Input targetInput;
        #endregion

        #region Builtin Methods
        private void OnEnable()
        {
            targetInput = (IP_XboxAirplane_Input)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            string debugInfo = "";
            debugInfo += "Pitch = " + targetInput.Pitch + "\n";
            debugInfo += "Roll = " + targetInput.Roll + "\n";
            debugInfo += "Yaw = " + targetInput.Yaw + "\n";
            debugInfo += "Throttle = " + targetInput.Throttle + "\n";
            debugInfo += "Brake = " + targetInput.Brake + "\n";
            debugInfo += "Flaps = " + targetInput.Flaps + "\n";

            GUILayout.Space(10);
            EditorGUILayout.TextArea(debugInfo, GUILayout.Height(100));
            GUILayout.Space(10);
            Repaint();
        }
        #endregion
    }
}

