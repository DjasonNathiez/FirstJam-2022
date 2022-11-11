using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [Header("Input")] 
    [SerializeField] private KeyCode leftInput;
    [SerializeField] private KeyCode rightInput;
    [Header("Stats")]
    [SerializeField] float moveSpeed = 1;

    private Vector3 position;
    
    public bool canMove = true;

    public GameObject neighboorLeft;
    public GameObject neighboorRight;
    public float neighboorMarge = 2;

     // en %

    private PhotonView view;

// Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        if(!view.IsMine)return;
    }

    // Update is called once per frame
    void Update()
    {
        if(!view.IsMine) return;
        if(CoreManager.Instance != null)position = CoreManager.Instance.transform.position;
        transform.position = position;
        if (canMove)Move();
    }

    void Move()
    {
        if (Input.GetKey(leftInput)) if( !(transform.localRotation.eulerAngles.z < neighboorLeft.transform.localRotation.eulerAngles.z && transform.localRotation.eulerAngles.z + moveSpeed + neighboorMarge > neighboorLeft.transform.localRotation.eulerAngles.z)) transform.Rotate(Vector3.forward * moveSpeed * Time.deltaTime);
        
        if (Input.GetKey(rightInput)) if(!(transform.localRotation.eulerAngles.z > neighboorRight.transform.localRotation.eulerAngles.z && transform.localRotation.eulerAngles.z - moveSpeed - neighboorMarge < neighboorRight.transform.localRotation.eulerAngles.z)) transform.Rotate(Vector3.back*moveSpeed * Time.deltaTime); 
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext("position");
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();
        }
    }
}
