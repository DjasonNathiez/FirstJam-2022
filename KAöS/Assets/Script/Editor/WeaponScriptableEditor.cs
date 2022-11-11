using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

[CustomEditor(typeof(WeaponScriptable))]
public class WeaponScriptableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WeaponScriptable script = (WeaponScriptable)target;

        script.rarity = (Enums.Rarity)EditorGUILayout.EnumPopup("Rarity", script.rarity);
        script.firerate = EditorGUILayout.Slider("Firerate",script.firerate,0.1f,5f);
        script.damage = EditorGUILayout.IntField("Damage", script.damage);
        EditorGUILayout.Space();
        if (script.coneShoot = EditorGUILayout.Toggle("Cone Shot", script.coneShoot))
        {
            script.paternAngle = EditorGUILayout.FloatField("Patern Angle", script.paternAngle);
            script.patern = EditorGUILayout.Toggle("Patern", script.patern);
        }
        EditorGUILayout.Space();
        script.bulletNumber = EditorGUILayout.IntField("Bullet Number", script.bulletNumber);
        if (script.bulletNumber != 1)
        {
            script.shotLatency = EditorGUILayout.Slider("Shot Latency", script.shotLatency, 0f, 0.5f);
            if (script.shotLatency > 0f) script.randomLatency = EditorGUILayout.Toggle("RandomLatency", script.randomLatency);
        }
        script.bulletSize = EditorGUILayout.Vector2Field("Bullet Size", script.bulletSize);
        script.moveShot = EditorGUILayout.FloatField("Move Shot", script.moveShot);
        script.bulletSpeed = EditorGUILayout.FloatField("Bullet Speed", script.bulletSpeed);
        script.speedCurve = EditorGUILayout.CurveField("Speed Curve", script.speedCurve);
        script.bulletLifetime = EditorGUILayout.FloatField("Bullet Lifetime", script.bulletLifetime);
        
        
        serializedObject.Update();
    }
}
