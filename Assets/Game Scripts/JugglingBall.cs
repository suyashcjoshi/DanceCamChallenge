using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugglingBall : MonoBehaviour
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

    public bool hasHold = false;
    public bool hasbeenDown = false;
    public bool hasbeenUp = false;
    public bool hasStopped = false;

    private Transform initialParent = null;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = GameObject.FindObjectsOfType<GameLogic>()[0].GetComponent<GameLogic>();
        rigidBody = GetComponent<Rigidbody>();
        initialParent = this.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasHold)
        {
            DetectHandVelocity detectHandVelocity = this.transform.parent.GetComponent<DetectHandVelocity>();
            if(detectHandVelocity)
            {
                if(detectHandVelocity.VelocityState == "UP")
                {
                    hasbeenUp = true;
                }
                else if(detectHandVelocity.VelocityState == "DOWN" && hasbeenUp == false)
                {
                    hasbeenDown = true;
                }
                else if(detectHandVelocity.VelocityState == "Rest" && hasbeenUp == true)
                {
                    hasStopped = true;
                }
            }
        }

        if(hasHold && hasbeenUp && hasStopped)
        {
            Throw();
            ResetBallGestureSequence();
        }
        else if(hasHold && hasbeenDown && hasbeenUp && hasStopped)
        {
            Throw();
            ResetBallGestureSequence();
        }
    }

    // The ball will be hold on a REST hand, throw in the air if hand goes UP
    //
    // Normal Throw: REST > UP > REST
    // POWER Throw: REST > DOWN > UP > REST
    //
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("We are colliding " + collision.collider.gameObject.name);

        // Lose a score if fell on floor
        if (collision.collider.gameObject.name == floorGameObjects.name)
        {
            gameLogic.losingScore(1);
        }


        foreach (GameObject go in activeGameObjects)
        {
            if (go.name == collision.collider.gameObject.name)
            {
                if(hasHold == false)
                {
                    Debug.Log("We are attaching to " + go.name);

                    // Attach self to the hand
                    this.transform.SetParent(go.transform, true);
                    rigidBody.isKinematic = true;

                    DetectHandVelocity detectHandVelocity = this.transform.parent.GetComponent<DetectHandVelocity>();
                    detectHandVelocity.attachedObject = this.gameObject;

                    ResetBallGestureSequence();
                    hasHold = true;
                    break;
                }
            }
        }
    }

    private void Throw()
    {
        // Bounce now
        rigidBody.isKinematic = false;
        rigidBody.velocity = new Vector3(Random.Range(bouncingOffRange, -bouncingOffRange), bouncingVelocity, 0);

        DetectHandVelocity detectHandVelocity = this.transform.parent.GetComponent<DetectHandVelocity>();
        detectHandVelocity.attachedObject = null;

        this.transform.SetParent(initialParent, true);
    }

    private void ResetBallGestureSequence()
    {
        hasHold = false;
        hasbeenDown = false;
        hasbeenUp = false;
        hasStopped = false;
    }
}
