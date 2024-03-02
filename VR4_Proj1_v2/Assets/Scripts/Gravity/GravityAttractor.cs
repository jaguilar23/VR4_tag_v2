using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -10f;

    public void Attract(Transform body)
    {
        float distanceFromCenter = Vector3.Distance(body.position, transform.position); // distance from center of gravity object

        if (distanceFromCenter <= 100f)
        {
            Vector3 targetDir = (body.position - transform.position).normalized;
            Vector3 bodyUp = body.up;

            //Debug.Log(targetDir.x + ", " + targetDir.y + ", " + targetDir.z);

            body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
            body.GetComponent<Rigidbody>().AddForce(targetDir * gravity);
        }
    }
}
