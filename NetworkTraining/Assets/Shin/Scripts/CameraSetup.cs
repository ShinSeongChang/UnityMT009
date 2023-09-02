using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 새로 추가
using Cinemachine;
using Photon.Pun;
using UnityChan;

public class CameraSetup : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        // 만약 자신이 로컬 플레이어라면
        if(photonView.IsMine)
        {
            // 씬에 있는 시네머신 가상 카메라를 찾고           
            Camera.main.transform.position = transform.GetChild(3).transform.position;

            Camera.main.transform.parent = transform.GetChild(3).transform;

            //GameObject.Find("Camera").GetComponent<Camera>();
            //CameraControll follow = new CameraControll();

            //follow.FollowCam = transform.GetChild(3).GetComponent<GameObject>();

            //Debug.Log(transform.GetChild(3).name);
            
            // 자신을 바라보며 따라오게 한다.
            //followCam.Follow = transform;
            //followCam.LookAt = transform;
        }
    }

}
