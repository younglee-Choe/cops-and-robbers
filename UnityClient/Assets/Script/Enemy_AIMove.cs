using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AIMove : MonoBehaviour
{
    public string targetObjectName = "Player";
    public float walkSpeed = 3;

    Vector3 direction;
    Vector3 moveVec;

    bool isAction;
    bool isWalking;
    bool isLookingAround;

    public float idleTime = 5;
    public float walkTime = 10;
    public float lookAroundTime = 3;

    float currentTime;
    float distance;

    GameObject targetObject;
    Animator anim;
    Rigidbody rbody;

    void Start()
    {
        currentTime = idleTime;
        isAction = true;

        targetObject = GameObject.Find(targetObjectName);

        anim = this.GetComponent<Animator>();
        rbody = this.GetComponent<Rigidbody>();
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        distance = Vector3.Distance(this.transform.position, targetObject.transform.position);

        if(distance < 5) {
            FindTarget();
        } else {
            Move();
            // Rotation();
            ElapseTime();
        }
    }

    // void OnCollisionEnter(Collision collision) {
    //     if(collision.collider.gameObject.CompareTag("CustomObject")) {
    //         Debug.Log("Collision");
    //     }
    // }

    private void FindTarget() {
        Debug.Log("🔍 Found the player");
        TryWalk();

        Vector3 direction = (targetObject.transform.position - this.transform.position);
        Vector3 moveVec = direction.normalized;
        transform.position += moveVec * walkSpeed * Time.deltaTime;
        transform.LookAt(transform.position +  moveVec);

        rbody.MovePosition(transform.position + transform.forward * walkSpeed * Time.deltaTime);
    }

    private void Move()
    {
        if (isWalking)
            rbody.MovePosition(transform.position + transform.forward * walkSpeed * Time.deltaTime);
    }

    private void Rotation()
    {
        if (isWalking)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), 0.01f);
            rbody.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();
        }
    }

    private void ReSet()
    {
        isWalking = false;
        isAction = true;
        anim.SetBool("isWalking", isWalking);

        // direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 2);

        if (_random == 0)
            TryWalk();
        else if (_random == 1)
            LookAround();
        // else if (_random == 2)
        //     Idle();
    }

    private void Idle()
    {
        currentTime = idleTime;
        isWalking = false;
        isLookingAround = false;
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isLookingAround", isLookingAround);
        Debug.Log("Breathe");
    }

    private void TryWalk()
    {
        currentTime = walkTime;
        isWalking = true;
        isLookingAround = false;
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isLookingAround", isLookingAround);
        Debug.Log("Walking");
    }

    private void LookAround()
    {
        currentTime = lookAroundTime;
        isWalking = false;
        isLookingAround = true;
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isLookingAround", isLookingAround);
        Debug.Log("Look around");
    }
}
