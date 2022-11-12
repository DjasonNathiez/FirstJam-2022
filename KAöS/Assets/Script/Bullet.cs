using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public WeaponScriptable script;
    public PlayerStats sender;
    
    private float timer;

    private bool enable;

    private void OnEnable()
    {
        timer = script.bulletLifetime;
        if (enable) Enable();
    }

    public void Enable()
    {
        timer = script.bulletLifetime;
        transform.localScale = new Vector2(script.bulletSize.x*.05f*sender.sizeRatio, script.bulletSize.y*.1f*sender.sizeRatio);
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
        transform.position += transform.up * script.bulletSpeed * script.speedCurve.Evaluate(Mathf.InverseLerp(script.bulletLifetime,0,timer)) ;
        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        PoolManager.Instance.Enqueue(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable iD = other.GetComponent<IDamageable>();
        int damage = (int)(script.damage * sender.damageRatio);
        damage = (int)(damage * (Random.Range(0f, 100f) < sender.critChance ? sender.critDamage : 1));
        if(iD != null) iD.TakeDamage(damage);
    }
}
