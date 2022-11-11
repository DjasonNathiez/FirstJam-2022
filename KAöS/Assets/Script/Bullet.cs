using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public WeaponScriptable script;
    
    private float timer;

    private bool enable;

    private void OnEnable()
    {
        timer = script.bulletLifetime;
    }

    public void Enable()
    {
        timer = script.bulletLifetime;
        transform.localScale = new Vector2(script.bulletSize.x*.05f, script.bulletSize.y*.1f);
        enable = true;
    }

    private void Update()
    {
        if (!enable) return;
        timer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(!enable)return;
        Debug.Log(script.speedCurve.Evaluate(Mathf.InverseLerp(script.bulletLifetime,0,timer)));
        transform.position += transform.up * script.bulletSpeed * script.speedCurve.Evaluate(Mathf.InverseLerp(script.bulletLifetime,0,timer)) ;
        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        //PoolManager.Instance.Enqueue(this);
    }
}
