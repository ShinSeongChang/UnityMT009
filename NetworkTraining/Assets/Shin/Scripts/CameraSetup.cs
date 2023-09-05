using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �߰�
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

        // ���� �ڽ��� ���� �÷��̾���
        if (photonView.IsMine)
        {
            // ���� �ִ� �ó׸ӽ� ���� ī�޶� ã��

            playerCam.transform.position = new Vector3(playerPos.x, playerPos.y + 10f, playerPos.z - 8f);
            playerCam.transform.parent = transform.GetChild(1).transform;

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
