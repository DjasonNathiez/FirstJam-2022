using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStats
    {
        public float firerateRatio = 1; //Ratio
        public float damageRatio = 1;// Ratio
        public float sizeRatio = 1; // ratio
        public float multiCastChance = 0; //en %
        public float critChance = 0; // en %
        public float critDamage = 1.5f; //Ratio
    }

public class ShootController : MonoBehaviour
{
    
    [Serializable]
    struct WeaponGizmos
    {
        public WeaponScriptable scriptable;
        //public bool showGizmoos;

        [HideInInspector] public float cooldown;
    }

    [SerializeField] private Transform shootTransform;
    [SerializeField] List<WeaponGizmos> weapons;
    List<float> timers = new List<float>();

    

    private PlayerStats stats = new PlayerStats();
    public List<StatScriptable> statModifiers = new List<StatScriptable>();


    private void Start()
    {
        timers = new List<float>();
        for (int i = 0; i < weapons.Count; i++)
        {
            timers.Add(weapons[i].scriptable.firerate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            timers[i] -= Time.deltaTime;
            if(timers[i] <= 0)
            {
                Shoot(weapons[i].scriptable);
                timers[i] = weapons[i].scriptable.firerate/stats.firerateRatio;
            }
        }
    }

    void Shoot(WeaponScriptable scriptable)
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        int totalbullet = scriptable.bulletNumber;
        if (stats.multiCastChance >= 100) totalbullet += (int)(stats.multiCastChance / 100);
        totalbullet += Random.Range(0f, 100f) < (stats.multiCastChance % 100) ? 1 : 0;
        
        if(CoreManager.Instance != null)CoreManager.Instance.Impulse(-(shootTransform.position-transform.position), scriptable.moveShot); 
        
        if (scriptable.coneShoot)
        {
            if (scriptable.patern)
            {
                rotation -= new Vector3(0, 0, scriptable.paternAngle / 2);
                for (int i = 0; i < totalbullet; i++)
                {
                    float newAngle = rotation.z + i * (scriptable.paternAngle / (totalbullet - 1));
                    Vector3 rota = new Vector3(0, 0, newAngle);
                    PoolManager.Instance.PoolInstantiate(scriptable, stats, shootTransform.position, rota);
                    
                }
            }
            else
            {
                StartCoroutine(MultiShot(scriptable, totalbullet));
            }
        }
        else if (totalbullet == 1)
        {
            if(PoolManager.Instance != null)PoolManager.Instance.PoolInstantiate(scriptable, stats, shootTransform.position, rotation);
            
        }
        else
        {
            StartCoroutine(MultiShot(scriptable, totalbullet));
        }
    }


    public void AddModifier(StatScriptable script)
    {
        statModifiers.Add(script);
        UpdateModifier();
    }

    public void UpdateModifier()
    {
        stats = new PlayerStats();
        foreach (var script in statModifiers)
        {
            if(script!=null)
            foreach (var mod in script.modifiers)
            {
                switch (mod.stat)
                {
                    case Enums.Stats.firerateRatio:
                        stats.firerateRatio *= mod.value;
                        break;
                    case Enums.Stats.damageRatio:
                        stats.damageRatio *= mod.value;
                        break;
                    case Enums.Stats.sizeRatio:
                        stats.sizeRatio *= mod.value;
                        break;
                    case Enums.Stats.multiCastChance:
                        stats.multiCastChance += mod.value;
                        break;
                    case Enums.Stats.criticalChance:
                        stats.critChance += mod.value;
                        break;
                    case Enums.Stats.criticalDamage:
                        stats.critDamage += mod.value;
                        break;

                }
            }
        }
    }
    
    IEnumerator MultiShot(WeaponScriptable script, int totalBullet )
    {
        Vector3 rotation =script.coneShoot ? transform.rotation.eulerAngles - new Vector3(0, 0, script.paternAngle / 2) : transform.rotation.eulerAngles;
        for (int i = 0; i < script.bulletNumber; i++)
        {
            if (script.coneShoot)
            {
                Vector3 rota = new Vector3(0, 0, rotation.z + Random.Range(0f, script.paternAngle));
                PoolManager.Instance.PoolInstantiate(script, stats, shootTransform.position, rota);
            }
            else PoolManager.Instance.PoolInstantiate(script, stats, shootTransform.position, rotation);

            yield return new WaitForSeconds(script.randomLatency ? Random.Range(0f, script.shotLatency): script.shotLatency );
        }
    }

}
