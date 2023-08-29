using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody myRigid;

    private float speed = 1000f;
    private float jumpForce = 1000f;

    public bool isJump = false;

    // Update is called once per frame
    void Update()
    {
        float zSpeed = Input.GetAxis("Vertical");
        float xSpeed = Input.GetAxis("Horizontal");
        float xMouse = Input.GetAxis("Mouse X");
        float jump = Input.GetAxis("Jump");

        if(zSpeed != 0 || xSpeed != 0)
        {
            Vector3 move = new Vector3(xSpeed * speed * Time.deltaTime, myRigid.velocity.y, zSpeed * speed * Time.deltaTime);
            myRigid.velocity = move;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 10f * Time.deltaTime);
        }

        //Debug.Log("ÄõÅÍ´Ï¾ð °ª : " + Quaternion.Slerp(transform.rotation, transform.rotation, speed));

        if (jump != 0 && isJump == false)
        {
            myRigid.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            isJump = true;
        }

        Debug.Log(Time.deltaTime);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isJump = false;
        }
    }
}
