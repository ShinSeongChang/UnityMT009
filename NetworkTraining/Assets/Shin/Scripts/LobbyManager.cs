using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 새로추가
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
        // 접속에 필요한 게임버전 설정 ( 같은 버전의 게임들끼리만 서버가 잡힘 )
        PhotonNetwork.GameVersion = gameVersion;
        // 설정한 정보로 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        // 마스터 서버 접속중 잠시 조인버튼 비활성화, 마스터 서버 접속중인 텍스트 띄우기
        JoinButton.interactable = false;
        ConnectionInfoText.text = "마스터서버 접속중...";
    }

    // OnConnectedToMaster 는 마스터 서버 접속에 성공한 경우 == > PhotonNetwork.ConnnectUsingSettings()가 실행 된 이후 자동으로 실행 됨.        
    public override void OnConnectedToMaster()
    {
        // 조인버튼 활성화
        JoinButton.interactable = true;

        // 접속 정보 표시
        ConnectionInfoText.text = " 오프라인 : 마스터 서버와 연결되지않음 \n 접속 재시도중 ...";

        // 마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    // 조인버튼을 클릭했을시 실행할 메서드
    public void Connect()
    {
        // 중복접속을 막기위해 조인버튼 비활성화
        JoinButton.interactable = false;

        // 마스터서버 접속중인 상태라면
        if(PhotonNetwork.IsConnected)
        {
            ConnectionInfoText.text = "룸 접속중...";

            // 랜덤룸 접속 실행
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            ConnectionInfoText.text = " 오프라인 : 마스터 서버와 연결되지않음 \n 접속 재시도중 ...";

            // 마스터 서버를 접속중인 상태가 아니라면 다시 마스터 서버 접속실행
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // 참가 가능한 방이 없는 경우 ( 빈 방이 없는 경우를 말함 )
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        ConnectionInfoText.text = "빈 방이 없음, 새로운방 생성...";

        // 최대 4명 수용가능한 방을 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    // 방 참가에 성공한다면
    public override void OnJoinedRoom()
    {
        ConnectionInfoText.text = "방 참가 성공";

        // 모든 룸 참가자가 해당하는 씬을 로드하게 함.
        // 기존 씬매니저를 통한 로드씬과 다른것은 룸의 플레이어들이 동기화된 채로 방장과 같은 씬으로 넘어간다는 것.
        PhotonNetwork.LoadLevel("MainScene");
    }
}
