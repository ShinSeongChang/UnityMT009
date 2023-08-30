using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �߰�
using Cinemachine;
using Photon.Pun;

public class CameraSetup : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        // ���� �ڽ��� ���� �÷��̾���
        if(photonView.IsMine)
        {
            // ���� �ִ� �ó׸ӽ� ���� ī�޶� ã��           
            CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();

            // �ڽ��� �ٶ󺸸� ������� �Ѵ�.
            followCam.Follow = transform;
            followCam.LookAt = transform;
        }
    }
}
