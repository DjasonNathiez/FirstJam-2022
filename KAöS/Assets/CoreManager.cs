using System;
using Photon.Pun;
using UnityEngine;

public class CoreManager : MonoBehaviourPunCallbacks,IDamageable, IPunObservable
{
    public static CoreManager Instance;
    
    private Rigidbody2D rb;
    
    public int currentLife;
    public float experience;
    public float coreToLevelUp = 100;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;

        FindObjectOfType<NetworkManager>().core = this;
        FindObjectOfType<NetworkManager>().hubGo.SetActive(true);
    }

    private void Start()
    {
        Debug.Log("Create");
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetExperience(int amount)
    {
        experience += amount;
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
            stream.SendNext("experience");
        }
        else
        {
            currentLife = (int)stream.ReceiveNext();
            experience = (int)stream.ReceiveNext();
        }
    }
}
