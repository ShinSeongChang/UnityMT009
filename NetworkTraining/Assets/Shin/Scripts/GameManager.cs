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
        // 플레이어 랜덤 위치생성할 좌표
        Vector3 randomSpawnPos = new Vector3(74f, 25f, 41f);
        //Debug.Log(randomSpawnPos);

        // y값은 0으로 해준다.
        //randomSpawnPos.y = 0f;

        // 네트워크상 모든 참가자의 방에 생성
        PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
    }

    void Update()
    {
        // 플레이어가 ESC키를 누르면
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // 해당 방을 떠나게 된다.
            PhotonNetwork.LeaveRoom();
        }
    }

    // 해당 방을떠나게 되면 자동으로 실행될 함수 OnLeftRoom
    public override void OnLeftRoom()
    {
        // 해당 방을 떠나면 로비씬으로 가게 된다.
        SceneManager.LoadScene("LobbyScene");
    }
}
