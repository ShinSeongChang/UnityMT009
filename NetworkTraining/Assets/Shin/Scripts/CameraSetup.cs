using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 새로 추가
using Cinemachine;
using Photon.Pun;
using UnityChan;

public class CameraSetup : MonoBehaviourPun
{
    //private PhotonView pv;
    private CinemachineVirtualCamera playerCam;
    Transform camerafoward;

    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = FindObjectOfType<CinemachineVirtualCamera>();
        playerPos = transform.GetChild(0).transform.position;
        camerafoward = playerCam.transform;
        //pv = GetComponent<PhotonView>();

        // 만약 자신이 로컬 플레이어라면
        if (photonView.IsMine)
        {
            // 씬에 있는 시네머신 가상 카메라를 찾고

            playerCam.transform.position = new Vector3(playerPos.x, playerPos.y + 10f, playerPos.z - 8f);
            playerCam.transform.parent = transform.GetChild(1).transform;

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
