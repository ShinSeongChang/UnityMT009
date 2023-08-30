using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �����߰�
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public TextMeshProUGUI ConnectionInfoText;
    public Button JoinButton;
    
    void Start()
    {
        // ���ӿ� �ʿ��� ���ӹ��� ���� ( ���� ������ ���ӵ鳢���� ������ ���� )
        PhotonNetwork.GameVersion = gameVersion;
        // ������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();

        // ������ ���� ������ ��� ���ι�ư ��Ȱ��ȭ, ������ ���� �������� �ؽ�Ʈ ����
        JoinButton.interactable = false;
        ConnectionInfoText.text = "�����ͼ��� ������...";
    }

    // OnConnectedToMaster �� ������ ���� ���ӿ� ������ ��� == > PhotonNetwork.ConnnectUsingSettings()�� ���� �� ���� �ڵ����� ���� ��.        
    public override void OnConnectedToMaster()
    {
        // ���ι�ư Ȱ��ȭ
        JoinButton.interactable = true;

        // ���� ���� ǥ��
        ConnectionInfoText.text = " �������� : ������ ������ ����������� \n ���� ��õ��� ...";

        // ������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    // ���ι�ư�� Ŭ�������� ������ �޼���
    public void Connect()
    {
        // �ߺ������� �������� ���ι�ư ��Ȱ��ȭ
        JoinButton.interactable = false;

        // �����ͼ��� �������� ���¶��
        if(PhotonNetwork.IsConnected)
        {
            ConnectionInfoText.text = "�� ������...";

            // ������ ���� ����
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            ConnectionInfoText.text = " �������� : ������ ������ ����������� \n ���� ��õ��� ...";

            // ������ ������ �������� ���°� �ƴ϶�� �ٽ� ������ ���� ���ӽ���
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // ���� ������ ���� ���� ��� ( �� ���� ���� ��츦 ���� )
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        ConnectionInfoText.text = "�� ���� ����, ���ο�� ����...";

        // �ִ� 4�� ���밡���� ���� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    // �� ������ �����Ѵٸ�
    public override void OnJoinedRoom()
    {
        ConnectionInfoText.text = "�� ���� ����";

        // ��� �� �����ڰ� �ش��ϴ� ���� �ε��ϰ� ��.
        // ���� ���Ŵ����� ���� �ε���� �ٸ����� ���� �÷��̾���� ����ȭ�� ä�� ����� ���� ������ �Ѿ�ٴ� ��.
        PhotonNetwork.LoadLevel("MainScene");
    }
}
