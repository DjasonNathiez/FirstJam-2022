using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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
                timers[i] = weapons[i].scriptable.firerate;
            }
        }
    }

    void Shoot(WeaponScriptable scriptable)
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        if(CoreManager.Instance != null)CoreManager.Instance.Impulse(-(shootTransform.position-transform.position), scriptable.moveShot); 
        
        if (scriptable.coneShoot)
        {
            if (scriptable.patern)
            {
                rotation -= new Vector3(0, 0, scriptable.paternAngle / 2);
                for (int i = 0; i < scriptable.bulletNumber; i++)
                {
                    float newAngle = rotation.z + i * (scriptable.paternAngle / (scriptable.bulletNumber - 1));
                    Debug.Log($"rotation.z + i * (paternAngle / bulNumb-1):{rotation.z} + {i} * ({scriptable.paternAngle} / {scriptable.bulletNumber-1} ): {newAngle}");
                    Vector3 rota = new Vector3(0, 0, newAngle);
                    Debug.Log(rota);
                    PoolManager.Instance.PoolInstantiate(scriptable, shootTransform.position, rota);
                    
                }
            }
            else
            {
                StartCoroutine(MultiShot(scriptable));
            }
        }
        else if (scriptable.bulletNumber == 1)
        {
            if(PoolManager.Instance != null)PoolManager.Instance.PoolInstantiate(scriptable, shootTransform.position, rotation);
            
        }
        else
        {
            StartCoroutine(MultiShot(scriptable));
        }
    }

    IEnumerator MultiShot(WeaponScriptable script)
    {
        Vector3 rotation =script.coneShoot ? transform.rotation.eulerAngles - new Vector3(0, 0, script.paternAngle / 2) : transform.rotation.eulerAngles;
        for (int i = 0; i < script.bulletNumber; i++)
        {
            if (script.coneShoot)
            {
                Vector3 rota = new Vector3(0, 0, rotation.z + Random.Range(0f, script.paternAngle));
                PoolManager.Instance.PoolInstantiate(script, shootTransform.position, rota);
            }
            else PoolManager.Instance.PoolInstantiate(script, shootTransform.position, rotation);

            yield return new WaitForSeconds(script.randomLatency ? Random.Range(0f, script.shotLatency): script.shotLatency );
        }
    }

}
