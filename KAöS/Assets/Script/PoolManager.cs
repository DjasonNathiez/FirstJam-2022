using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    
    public enum bulletType
    {
        basic, wave
    }
    
    [Serializable]
    struct BulletStruct
    {
        public GameObject prefab;
        public WeaponScriptable scriptable;
        //public bulletType type;
    }

    [SerializeField] private BulletStruct[] bullets;

    private Dictionary<WeaponScriptable, GameObject> prefabDictionary;
    private Dictionary<WeaponScriptable, Queue<GameObject>> queueDictionary;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }


    private void Start()
    {
        prefabDictionary = new Dictionary<WeaponScriptable, GameObject>();
        queueDictionary = new Dictionary<WeaponScriptable, Queue<GameObject>>();
        foreach (var bullet in bullets)
        {
            prefabDictionary.Add(bullet.scriptable, bullet.prefab);
            queueDictionary.Add(bullet.scriptable, new Queue<GameObject>());
        }
    }


    public void PoolInstantiate(WeaponScriptable weapon, Vector3 position, Vector3 rotation)
    {
        //Debug.Log("Instantiate");
        GameObject go = null;
        if (queueDictionary[weapon].Count == 0)
        {
           go = Instantiate(prefabDictionary[weapon], position, Quaternion.Euler(rotation), transform);
           Bullet bul = go.GetComponent<Bullet>();
           bul.script = weapon;
           bul.Enable();
        }
        else
        {
            go = queueDictionary[weapon].Dequeue();
            go.transform.position = position;
            go.transform.rotation = Quaternion.Euler(rotation);
            go.SetActive(true);
        }

    }

    public void Enqueue(Bullet bullet)
    {
        queueDictionary[bullet.script].Enqueue(bullet.gameObject);
    }
}
