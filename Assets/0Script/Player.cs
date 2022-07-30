using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    float hAxis;
    float vAxis;
    bool walkButtonDown;
    bool jumpButtonDown;
    bool isJumped;
    bool isDodge;


    Vector3 moveVector;
    Vector3 dodgeVector;

    Animator playerAnim;

    Rigidbody rigidBody;

    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");     // Axis ���� ������ ��ȯ
        vAxis = Input.GetAxisRaw("Vertical");       // 
        walkButtonDown = Input.GetButton("Walk");

        jumpButtonDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        moveVector = new Vector3(hAxis, 0, vAxis).normalized; // h, v�� ���� Vector ���� h.v���� ũ�� ������ normal ������ ��ȯ

        if (isDodge)
            moveVector = dodgeVector;

        if (walkButtonDown)
        {
            transform.position += moveVector * speed * 0.3f * Time.deltaTime;
        }
        else
        {
            transform.position += moveVector * speed * Time.deltaTime;
        }


        playerAnim.SetBool("isRun", moveVector != Vector3.zero);
        playerAnim.SetBool("isWalk", walkButtonDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVector);
    }

    void Jump()
    {
        // �������� �ʰ� ���� ���� ����
        if (jumpButtonDown && moveVector == Vector3.zero && !isJumped && !isDodge)
        {
            // ���� ���̿� ���� ��� �ΰ���
            // 1. Project Settings-Physics-Gravity(Y)
            // 2. Power 
            int jumpPower = 15;
            rigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            // Animation
            playerAnim.SetBool("isJumped", true);
            playerAnim.SetTrigger("doJump");


            isJumped = true;
        }
    }

    void Dodge()
    {
        // �����̴� ���¿����� Dodge
        if (jumpButtonDown && moveVector != Vector3.zero && !isDodge && !isJumped)
        {
            dodgeVector = moveVector;
            speed *= 2;

            // Animation
            playerAnim.SetTrigger("doDodge");

            isDodge = true;
            // �ð��� ȣ��
            Invoke("DodgeFinished", 0.5f);
        }
    }

    void DodgeFinished()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumped = false;
            playerAnim.SetBool("isJumped", false);
        }
    }
}
