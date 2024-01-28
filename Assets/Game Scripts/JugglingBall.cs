using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugglingBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The ball will bounce up when hit by hand
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("We are colliding " + collision.collider.gameObject.name);

        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
        {
            audioSource.Play();
            Debug.Log("And hits hard.");
        }
    }
}
