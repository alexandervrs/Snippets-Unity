
/**
 * Light.cs
 * Light related snippets for Unity
 */

/* using */
using UnityEngine;


/* -----------------------------------------
   Create Point Light
----------------------------------------- */
private GameObject lightObject;
private Light      lightComponent;

lightObject                    = new GameObject("PointLight");
lightObject.transform.position = new Vector3(0.14f, 0.08f, -101.87f);

lightComponent                 = lightObject.AddComponent<Light>();
lightComponent.color           = Color.white;
lightComponent.range           = 90.0f;
lightComponent.intensity       = 1.0f;
lightComponent.renderMode      = LightRenderMode.Auto;

lightObject.SetActive(true);