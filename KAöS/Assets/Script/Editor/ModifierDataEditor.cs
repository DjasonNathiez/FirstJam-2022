
/*
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ModifierData))]
public class ModifierDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ModifierData script = (ModifierData)target;
        switch (script.stat = (Enums.Stats)EditorGUILayout.EnumPopup("Stat", script.stat))
        {
            case Enums.Stats.firerateRatio : 
                script.value = 1;
                script.value = EditorGUILayout.FloatField("value", script.value);
                break;
            case Enums.Stats.damageRatio : 
                script.value = 1;
                script.value = EditorGUILayout.FloatField("value", script.value);
                break;
            case Enums.Stats.sizeRatio : 
                script.value = 1;
                script.value = EditorGUILayout.FloatField("value", script.value);
                break;
            case Enums.Stats.criticalDamage : 
                script.value = 1;
                script.value = EditorGUILayout.FloatField("value", script.value);
                break;
            case Enums.Stats.firerateAdd : 
                script.value = 0;
                script.value = EditorGUILayout.FloatField("value", script.value);
                break;
            case Enums.Stats.damageAdd : 
                script.value = 0;
                script.value = EditorGUILayout.FloatField("value", script.value);
                break;
            case Enums.Stats.multiCastChance : 
                script.value = 0;
                script.value = EditorGUILayout.FloatField("value", script.value);
                break;
            case Enums.Stats.criticalChance : 
                script.value = 0;
                script.value = EditorGUILayout.FloatField("value", script.value);
                break;


        }
    }
}
*/
