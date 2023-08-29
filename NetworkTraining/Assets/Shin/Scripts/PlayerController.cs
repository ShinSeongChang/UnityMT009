using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody myRigid;
    public bool isJump = false;

    // Update is called once per frame
    void Update()
    {
        float zSpeed = Input.GetAxis("Vertical");
        float xSpeed = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        float xMouse = Input.GetAxis("Mouse X");

        //Debug.Log(jump);

        Vector3 move = new Vector3(xSpeed * 10f, myRigid.velocity.y, zSpeed * 10f);
        myRigid.velocity = move;

        transform.forward = Vector3.Lerp(transform.forward, move, Time.deltaTime);
        //transform.rotation = Quaternion.Lerp()
        //transform.rotation = Quaternion.LookRotation(new Vector3(xMouse, 0f, 0f));



        //if (zSpeed != 0)
        //{
        //    myRigid.velocity = new Vector3(myRigid.velocity.x, myRigid.velocity.y, zSpeed * 10f);
        //}

        //if (xSpeed != 0)
        //{
        //    myRigid.velocity = new Vector3(xSpeed * 10f, myRigid.velocity.y, myRigid.velocity.z);
        //}

        if (jump != 0 && isJump == false)
        {
            myRigid.AddForce(new Vector3(myRigid.velocity.x, 10f, myRigid.velocity.z), ForceMode.Impulse);
            isJump = true;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isJump = false;
        }
    }
}
