using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileControl : MonoBehaviour
{
    public float throwForce = 100f;
    public float throwDirectionX = 0.17f;
    public float throwDirectionY = 0.67f;
    public Vector3 ballCameraOffset = new Vector3(0f, -1.4f, 2f);

    private Vector3 startPos;
    private Vector3 direction;
    private float startTime;
    private float endTime;
    private float duration;
    private bool directionChosen = false;
    private bool throwStarted = false;
    private Rigidbody rigidBody;

    [SerializeField]
    GameObject ARCam;

    


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        ARCam = GameObject.Find("ARCamera").gameObject;
        this.transform.parent = ARCam.transform;
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            startTime = Time.time;
            throwStarted = true;
            directionChosen = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            duration = endTime - startTime;
            direction = Input.mousePosition - startPos;
            directionChosen = true;
        }
        if (directionChosen)
        {
            rigidBody.mass = 1;
            rigidBody.useGravity = true;
            rigidBody.AddForce(
                ARCam.transform.forward * throwForce / duration + 
                ARCam.transform.up * direction.y * throwDirectionY +
                ARCam.transform.right * direction.x * throwDirectionX);

            startTime = 0.0f;
            duration =  0.0f;

            startPos = new Vector3(0, 0, 0);
            direction = new Vector3(0, 0, 0);

            throwStarted = false;
            directionChosen  = false;

        }

        if (Time.time - endTime >= 5 && Time.time - endTime <=6)
        {
            ResetBall();
        }
    }

    public void ResetBall()
    {
        rigidBody.mass = 0;
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        endTime = 0.0f;

        Vector3 ballPos = ARCam.transform.position  + ARCam.transform.forward * ballCameraOffset.z + ARCam.transform.up * ballCameraOffset.y;
        this.transform.position = ballPos;
    }
}
