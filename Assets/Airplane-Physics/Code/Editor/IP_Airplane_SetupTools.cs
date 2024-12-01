using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndiePixel
{
    public static class IP_Airplane_SetupTools
    {
        //Create root GO
        public static void BuildDefaultAirplane(string aName) {
            GameObject rootGO = new GameObject(aName,
                typeof(IP_Airplane_Controller),
                typeof(IP_BaseAirplane_Input));
            GameObject cogGO = new GameObject("COG");

            //Create Center of Gravity
            cogGO.transform.SetParent(rootGO.transform, false);

            //Create the Base Components or Find them
            IP_BaseAirplane_Input input = rootGO.GetComponent<IP_BaseAirplane_Input>();
            IP_Airplane_Controller controller = rootGO.GetComponent<IP_Airplane_Controller>();
            IP_Airplane_Characteristics characteristics = rootGO.GetComponent<IP_Airplane_Characteristics>();


            //Setup the Airplane
            if (controller) {
                controller.input = input;
                controller.characteristics = characteristics;
                controller.centerOfGravity = cogGO.transform;

                //Create Structure
                GameObject graphicsGroup = new GameObject("Graphics_GRP");
                GameObject collisionGroup = new GameObject("Collision_GRP");
                GameObject controlSurfaceGroup = new GameObject("ControlSurfaces_GRP");

                graphicsGroup.transform.SetParent(rootGO.transform, false);
                collisionGroup.transform.SetParent(rootGO.transform, false);
                controlSurfaceGroup.transform.SetParent(rootGO.transform, false);

                //Create First Engine
                GameObject engineGO = new GameObject("Engine",typeof(IP_Airplane_Engine));
                IP_Airplane_Engine engine = engineGO.GetComponent<IP_Airplane_Engine>();
                controller.engines.Add(engine);
                engineGO.transform.SetParent(rootGO.transform, false);

                //Create the base Airplane
                GameObject defaultAirplane = (GameObject)AssetDatabase.LoadAssetAtPath(
                    "Assets/Airplane-Physics/Art/Objects/Airplanes/Indie-Pixel_Airplane/IndiePixel_Airplane.fbx",
                    typeof(GameObject)
                    );
                if (defaultAirplane) {
                    GameObject.Instantiate(defaultAirplane, graphicsGroup.transform);
                }

                //Select the airplane setup
                Selection.activeGameObject = rootGO;
            }
        }
    }

}