using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    private Rigidbody myRigid;
    private Animator myAni = default;

    private float speed = 500f;
    private float jumpForce = 500f;

    public bool isJump = false;

    void Start()
    {
        myRigid = transform.GetComponent<Rigidbody>();
        myAni = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // �ڽ��� �����÷��̾ �ƴ϶��
        if (photonView.IsMine == false)
        {
            // Ű�Է��� ���� �ʴ´�.
            return;
        }

        float zSpeed = Input.GetAxis("Vertical");
        float xSpeed = Input.GetAxis("Horizontal");        
        float jump = Input.GetAxis("Jump");

        if(zSpeed != 0 || xSpeed != 0)
        {
            // �̵�Ű�Է°��� ���밪���� ����ä�� �ִϸ����Ϳ� ������
            myAni.SetFloat("Move", Mathf.Abs(zSpeed) + Mathf.Abs(xSpeed));
             
            // ĳ���� ������ �̵���ǥ
            Vector3 move = new Vector3(xSpeed * speed * Time.deltaTime, 0f, zSpeed * speed * Time.deltaTime);

            // ============���߿� !!! ���� ��ǥ���� ������ǥ ������, �� ĳ���� �������� ������ ��´�.=============
            // �� �κ��� ������ ĳ���ʹ� ����Ű �Է½� �׻� ���� ���� z�������� ���ư��� ��.

            move = transform.TransformDirection(move);

            // ==========================================================================================

            // ĳ���� �̵�
            myRigid.velocity = move + (Vector3.up * myRigid.velocity.y);

            // ĳ���� ȸ��
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 3f * Time.deltaTime);
        }


        if (jump != 0 && isJump == false)
        {
            myAni.SetBool("isJump", true);
            myRigid.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            isJump = true;
        }

        myAni.SetFloat("Jump", Mathf.Abs(myRigid.velocity.y)); 

    }



    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];         

        if(collision.collider.tag == "Ground")
        {
            myAni.SetBool("isJump", false);
            isJump = false;
        }
    }
}
