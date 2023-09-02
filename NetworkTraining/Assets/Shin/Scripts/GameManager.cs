using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance = default;
    public GameObject playerPrefab = default;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ���� ��ġ������ ��ǥ
        Vector3 randomSpawnPos = new Vector3(74f, 25f, 41f);
        //Debug.Log(randomSpawnPos);

        // y���� 0���� ���ش�.
        //randomSpawnPos.y = 0f;

        // ��Ʈ��ũ�� ��� �������� �濡 ����
        PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
    }

    void Update()
    {
        // �÷��̾ ESCŰ�� ������
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // �ش� ���� ������ �ȴ�.
            PhotonNetwork.LeaveRoom();
        }
    }

    // �ش� ���������� �Ǹ� �ڵ����� ����� �Լ� OnLeftRoom
    public override void OnLeftRoom()
    {
        // �ش� ���� ������ �κ������ ���� �ȴ�.
        SceneManager.LoadScene("LobbyScene");
    }
}
