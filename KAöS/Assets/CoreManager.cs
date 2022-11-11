using System;
using Photon.Pun;
using UnityEngine;

public class CoreManager : MonoBehaviour,IDamageable, IPunObservable
{
    public static CoreManager Instance;

    private Rigidbody2D rb;
    public int currentLife;

    private void Awake()
    {
        
        if (Instance != null) return;
        Instance = this;
    }

    private void Start()
    {
        Debug.Log("Create");
        rb = GetComponent<Rigidbody2D>();
    }

    public void Impulse(Vector2 dir, float force)
    {
        rb.AddForce(dir*force);
    }

    public void TakeDamage(int amount)
    {
        currentLife -= amount;
    }
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext("currentLife");
        }
        else
        {
            //currentLife = (int)stream.ReceiveNext();
        }
    }
}
