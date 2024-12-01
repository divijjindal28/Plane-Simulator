using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndiePixel{
    public class IP_Airplane_SetupWindow : EditorWindow
    {
        #region Variables
        private string wantedName;
        #endregion

        #region Builtin Methods
        public static void LaunchSetupWindow() {
            //IP_Airplane_SetupWindow.GetWindow(typeof(IP_Airplane_SetupWindow),
            //    true, "Airplane Setup").Show();

            IP_Airplane_SetupWindow win = (IP_Airplane_SetupWindow)IP_Airplane_SetupWindow.GetWindow(typeof(IP_Airplane_SetupWindow),true, "Airplane Setup");
            win.Show();


        }


        private void OnGUI()
        {
            wantedName = EditorGUILayout.TextField("Airplane Name", wantedName);
            if (GUILayout.Button("Create New Airplane")) {
                IP_Airplane_SetupTools.BuildDefaultAirplane(wantedName);
                IP_Airplane_SetupWindow.GetWindow<IP_Airplane_SetupWindow>().Close();
            }
        }
        #endregion
    }
}
