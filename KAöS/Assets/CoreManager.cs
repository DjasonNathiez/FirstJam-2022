using Photon.Pun;
using UnityEngine;

public class CoreManager : MonoBehaviour,IDamageable, IPunObservable
{
    public int currentLife;
    
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
            currentLife = (int)stream.ReceiveNext();
        }
    }
}
