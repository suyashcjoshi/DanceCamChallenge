using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHandVelocity : MonoBehaviour
{
    private Rigidbody rigidBody;

    public Vector3 lastPosition = Vector3.zero;
    public Vector3 currentPosition = Vector3.zero;
    public Vector3 currentVelocity = Vector3.zero;

    public GameObject velocityIndicatorGameObject;
    public string VelocityState = "Rest";

    public int smoothingFactor = 3;
    public float restThreshold = 0.1f;

    public GameObject attachedObject = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = Vector3.Lerp(currentPosition, this.transform.position, 1.0f/smoothingFactor);  ;
        if (lastPosition != Vector3.zero)
        {
            currentVelocity = currentPosition - lastPosition;
            if(currentVelocity.magnitude < restThreshold)
            {
                currentVelocity = Vector3.zero;
                VelocityState = "Rest";
                if(attachedObject)
                {
                    if (attachedObject.GetComponent<MeshRenderer>() != null)
                    {
                        attachedObject.GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                }
            }
            else
            {
                if(currentVelocity.y > 0)
                {
                    VelocityState = "UP";
                    if (attachedObject)
                    {
                        if (attachedObject.GetComponent<MeshRenderer>() != null)
                        {
                            attachedObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                        }
                    }
                }
                else
                {
                    VelocityState = "DOWN";
                    if (attachedObject)
                    {
                        if (attachedObject.GetComponent<MeshRenderer>() != null)
                        {
                            attachedObject.GetComponent<MeshRenderer>().material.color = Color.red;
                        }
                    }
                }
            }
        }
        lastPosition = currentPosition;
    }
}
