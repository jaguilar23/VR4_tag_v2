using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
public class MultiplayerMovement : MonoBehaviour
{
    private PhotonView myView;
    private GameObject myChild;

    private float xInput;
    private float yInput;
    public float movementSpeed = 10.0f;

    private InputData inputData;
    //[SerializeField] private GameObject myObjectToMove;
    private Rigidbody myRB;
    private Transform myXRRig;

    // System vars
    bool grounded;
    public LayerMask groundedMask;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    // Start is called before the first frame update
    void Start()
    {
        myView = GetComponentInParent<PhotonView>();

        //myChild = transform.GetChild(0).gameObject;
        myRB = GetComponent<Rigidbody>();

        GameObject myXrOrigin = GameObject.Find("XR Origin (XR Rig)");
        myXRRig = myXrOrigin.transform;
        inputData = myXrOrigin.GetComponent<InputData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myView.IsMine)
        {
            myXRRig.position = transform.position;

            // TryGetFeatureValue is VERY useful for future development
            if (inputData.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
            {
                xInput = movement.x;
                yInput = movement.y;
            }
            else
            {
                xInput = Input.GetAxis("Horizontal");
                yInput = Input.GetAxis("Vertical");
            }
        }

        Vector3 moveDir = new Vector3(xInput, 0, yInput).normalized;
        Vector3 targetMoveAmount = moveDir * movementSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        //myRB.AddForce(xInput * movementSpeed, 0, yInput * movementSpeed);
        myRB.MovePosition(myRB.position + localMove);
    }
}
