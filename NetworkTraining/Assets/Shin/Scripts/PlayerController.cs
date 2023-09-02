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
        // 자신이 로컬플레이어가 아니라면
        if (photonView.IsMine == false)
        {
            // 키입력을 받지 않는다.
            return;
        }

        float zSpeed = Input.GetAxis("Vertical");
        float xSpeed = Input.GetAxis("Horizontal");        
        float jump = Input.GetAxis("Jump");

        if(zSpeed != 0 || xSpeed != 0)
        {
            // 이동키입력값의 절대값들을 더한채로 애니메이터에 보내기
            myAni.SetFloat("Move", Mathf.Abs(zSpeed) + Mathf.Abs(xSpeed));
             
            // 캐릭터 움직일 이동좌표
            Vector3 move = new Vector3(xSpeed * speed * Time.deltaTime, 0f, zSpeed * speed * Time.deltaTime);

            // ============개중요 !!! 월드 좌표값을 로컬좌표 값으로, 즉 캐릭터 기준으로 정면을 잡는다.=============
            // 이 부분이 없으면 캐릭터는 정면키 입력시 항상 월드 기준 z방향으로 나아가려 함.

            move = transform.TransformDirection(move);

            // ==========================================================================================

            // 캐릭터 이동
            myRigid.velocity = move + (Vector3.up * myRigid.velocity.y);

            // 캐릭터 회전
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
