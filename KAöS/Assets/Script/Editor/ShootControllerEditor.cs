using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShootController))]
public class ShootControllerEditor : Editor
{
   public override void OnInspectorGUI()
   {
      ShootController script = (ShootController)target;
      base.OnInspectorGUI();
      if (GUILayout.Button("Update Mod"))
      {
         script.UpdateModifier();
      }
   }
}
