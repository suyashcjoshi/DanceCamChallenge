using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    private GameLogic gameLogic;
    private Rigidbody rigidBody;

    [SerializeField]
    private GameObject[] activeGameObjects;

    [SerializeField]
    private float bouncingVelocity = 10.0f;

    [SerializeField]
    private float bouncingOffRange = 0.0f;

    [SerializeField]
    private GameObject floorGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindObjectsOfType<GameLogic>()[0].GetComponent<GameLogic>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The ball will bounce up when hit by hand
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        if(collision.collider.gameObject.name == floorGameObjects.name)
        {
            gameLogic.losingScore(1);
        }

        if (collision.relativeVelocity.magnitude > 2)
        {
            foreach(GameObject go in activeGameObjects)
            {
                if(go.name == collision.collider.gameObject.name)
                {

                    rigidBody.velocity = new Vector3(Random.Range(bouncingOffRange, -bouncingOffRange), bouncingVelocity, 0);
                    break;
                }
            }
        }
    }
}
