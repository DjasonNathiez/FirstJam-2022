using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [Header("Input")] 
    [SerializeField] private KeyCode leftInput;
    [SerializeField] private KeyCode rightInput;
    [Header("Stats")]
    [SerializeField] float moveSpeed = 1;
    
    public bool canMove = true;

    public GameObject neighboorLeft;
    public GameObject neighboorRight;
    public float neighboorMarge = 2;

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
        
        if (canMove)Move();
    }

    void Move()
    {
        Debug.Log($"!({transform.localRotation.eulerAngles.z} > {neighboorLeft.transform.localRotation.eulerAngles.z} && {transform.localRotation.eulerAngles.z - moveSpeed - neighboorMarge} < {neighboorLeft.transform.localRotation.eulerAngles.z})");
        if (Input.GetKey(leftInput)) if( !(transform.localRotation.eulerAngles.z < neighboorLeft.transform.localRotation.eulerAngles.z && transform.localRotation.eulerAngles.z + moveSpeed + neighboorMarge > neighboorLeft.transform.localRotation.eulerAngles.z)) transform.Rotate(Vector3.forward * moveSpeed);
        
        if (Input.GetKey(rightInput)) if(!(transform.localRotation.eulerAngles.z > neighboorRight.transform.localRotation.eulerAngles.z && transform.localRotation.eulerAngles.z - moveSpeed - neighboorMarge < neighboorRight.transform.localRotation.eulerAngles.z)) transform.Rotate(Vector3.back*moveSpeed); 
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
