using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScriptable : ScriptableObject
{
    public float firerate = 1f;
    public int damage = 10;
    public int bulletNumber = 1;
    public float shotLatency = .1f;
    public bool randomLatency;
    //Patern
    
    public bool coneShoot;
    public bool patern;
    public float paternAngle  = 45f;
    
    public Vector2 bulletSize = new Vector2(1,1);
    public float moveShot = 10f;
    
    public float bulletSpeed = .1f;
    public AnimationCurve speedCurve = AnimationCurve.Constant(0,1,1);
    
    public float bulletLifetime = 1f;
    private bool piercing = false;
    public bool actualise = false;

}
