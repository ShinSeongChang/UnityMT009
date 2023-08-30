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
        // 자신이 로컬플레이어가 아니라면
        if (photonView.IsMine == false)
        {
            // 키입력을 받지 않는다.
            return;
        }

        Vector3 vector = new Vector3(0f, myRigid.rotation.y, 0f);

        //transform.rotation = Quaternion.Euler(vector);
        //float Z = Input.GetAxis("Vertical");

        float zSpeed = Input.GetAxis("Vertical");
        float xSpeed = Input.GetAxis("Horizontal");        
        float jump = Input.GetAxis("Jump");
        //myAni.SetFloat("Move", 0f);

        //Debug.Log("노말 : " + Z);
        //Debug.Log("겟액시스 로우 ㅡㅡㅡㅡ : " + zSpeed);

        if(zSpeed != 0 || xSpeed != 0)
        {
            // 이동키입력값의 절대값들을 더한채로 애니메이터에 보내기
            myAni.SetFloat("Move", Mathf.Abs(zSpeed) + Mathf.Abs(xSpeed));

             
            Vector3 move = new Vector3(xSpeed * speed * Time.deltaTime, 0f, zSpeed * speed * Time.deltaTime);
            myRigid.velocity = move + (Vector3.up * myRigid.velocity.y);

            // 절대값 구하기
            //Debug.Log("GetAxis 값 : " + Mathf.Abs(zSpeed));

            // 벡터의 반환값??
            //Debug.Log(move.magnitude);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 5f * Time.deltaTime);
        }

        //Debug.Log("쿼터니언 값 : " + Quaternion.Slerp(transform.rotation, transform.rotation, speed));

        if (jump != 0 && isJump == false)
        {
            myAni.SetBool("isJump", true);
            myRigid.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            isJump = true;
        }

        myAni.SetFloat("Jump", Mathf.Abs(myRigid.velocity.y));
        //Debug.Log("점프 벨로시티 : " + Mathf.Abs(myRigid.velocity.y));
    }



    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        //Debug.Log("충돌한 부분 : " + collision.contacts[0].point);
        Debug.Log("충돌 ?? : " + contactPoint.point.normalized);             

        if(collision.collider.tag == "Ground")
        {
            myAni.SetBool("isJump", false);
            isJump = false;
        }
    }
}
