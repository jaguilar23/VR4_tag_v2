using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    GravityAttractor planet;
    Rigidbody rigidbody;

    public float timer = 1.0f;

    private void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        planet.Attract(transform);

        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            //Debug.Log(transform.rotation.x + ", " + transform.rotation.y + ", " + transform.rotation.z + ", " + transform.rotation.w);
            timer = 1.0f;
        }

    }
}
