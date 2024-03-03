using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
public class MultiplayerMovement : MonoBehaviour
{
    public PhotonView myView;
    private GameObject myChild;

    private float xInput;
    private float yInput;
    public float movementSpeed = 10.0f;

    private InputData inputData;
    //[SerializeField] private GameObject myObjectToMove;
    private Rigidbody myRB;
    private Transform myXRRig;
    public GameObject playerUI;
    private PlayerLabel pL;

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
        pL = GetComponent<PlayerLabel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myView.IsMine)
        {
            playerUI.SetActive(true); // make player's UI the only one visible for them

            myXRRig.position = transform.position + transform.up;
            myXRRig.rotation = transform.rotation;

            // TryGetFeatureValue is VERY useful for future development
            if (inputData.leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
            {
                xInput = movement.x;
                yInput = movement.y;
            }
            else
            {
                xInput = Input.GetAxis("Horizontal");
                yInput = Input.GetAxis("Vertical");
            }

            if (inputData.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 r_movement))
            {
                float xRotate = r_movement.x;
                transform.Rotate(Vector3.up * xRotate);
            }

            if (inputData.rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool t_pressed) && pL.numTeleports > 0)
            {
                if (t_pressed)
                {
                    Vector3 teleportPosition = transform.position + transform.forward * pL.teleportDistance;
                    transform.position = teleportPosition; // move the player

                    pL.numTeleports--;
                }
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
