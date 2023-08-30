using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    public Rigidbody myRigid;
    public Animator myAni = default;

    private float speed = 1000f;
    private float jumpForce = 1000f;

    public bool isJump = false;

    void Start()
    {
        //myAni = GetComponent<Animator>();
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

        Vector3 vector = new Vector3(0f, myRigid.rotation.y, 0f);

        //transform.rotation = Quaternion.Euler(vector);
        //float Z = Input.GetAxis("Vertical");

        float zSpeed = Input.GetAxis("Vertical");
        float xSpeed = Input.GetAxis("Horizontal");        
        float jump = Input.GetAxis("Jump");
        //myAni.SetFloat("Move", 0f);

        //Debug.Log("�븻 : " + Z);
        //Debug.Log("�پ׽ý� �ο� �ѤѤѤ� : " + zSpeed);

        if(zSpeed != 0 || xSpeed != 0)
        {
            // �̵�Ű�Է°��� ���밪���� ����ä�� �ִϸ����Ϳ� ������
            myAni.SetFloat("Move", Mathf.Abs(zSpeed) + Mathf.Abs(xSpeed));

             
            Vector3 move = new Vector3(xSpeed * speed * Time.deltaTime, 0f, zSpeed * speed * Time.deltaTime);
            myRigid.velocity = move + (Vector3.up * myRigid.velocity.y);

            // ���밪 ���ϱ�
            //Debug.Log("GetAxis �� : " + Mathf.Abs(zSpeed));

            // ������ ��ȯ��??
            //Debug.Log(move.magnitude);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 5f * Time.deltaTime);
        }

        //Debug.Log("���ʹϾ� �� : " + Quaternion.Slerp(transform.rotation, transform.rotation, speed));

        if (jump != 0 && isJump == false)
        {
            myAni.SetBool("isJump", true);
            myRigid.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            isJump = true;
        }

        myAni.SetFloat("Jump", Mathf.Abs(myRigid.velocity.y));
        //Debug.Log("���� ���ν�Ƽ : " + Mathf.Abs(myRigid.velocity.y));
    }



    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        //Debug.Log("�浹�� �κ� : " + collision.contacts[0].point);
        Debug.Log("�浹 ?? : " + contactPoint.point.normalized);             

        if(collision.collider.tag == "Ground")
        {
            myAni.SetBool("isJump", false);
            isJump = false;
        }
    }
}
