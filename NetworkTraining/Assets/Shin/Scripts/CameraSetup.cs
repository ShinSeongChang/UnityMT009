using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �߰�
using Cinemachine;
using Photon.Pun;
using UnityChan;

public class CameraSetup : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        // ���� �ڽ��� ���� �÷��̾���
        if(photonView.IsMine)
        {
            // ���� �ִ� �ó׸ӽ� ���� ī�޶� ã��           
            Camera.main.transform.position = transform.GetChild(3).transform.position;

            Camera.main.transform.parent = transform.GetChild(3).transform;

            //GameObject.Find("Camera").GetComponent<Camera>();
            //CameraControll follow = new CameraControll();

            //follow.FollowCam = transform.GetChild(3).GetComponent<GameObject>();

            //Debug.Log(transform.GetChild(3).name);
            
            // �ڽ��� �ٶ󺸸� ������� �Ѵ�.
            //followCam.Follow = transform;
            //followCam.LookAt = transform;
        }
    }

}
