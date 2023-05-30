using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonMiniGameManager : MonoBehaviourPunCallbacks
{
    public static PhotonMiniGameManager Instance;
    //버전 입력
    private readonly string version = "1.0f";
    //사용자 아이디 입력
    private string userId = ButtonEvent.nickName;
    private int playerNum;

    public Transform[] pos;
    void Awake(){
        Instance = this;
        //같은 룸의 유저들에게 자동으로 씬을 로딩
        PhotonNetwork.AutomaticallySyncScene = true;
        //같은 버전의 유저끼리 접속 허용
        PhotonNetwork.GameVersion = version;
        //유저 아이디 할당
        PhotonNetwork.NickName = userId;
        //포톤 서버와 통신 횟수 설정. 초당 30회
        Debug.Log(PhotonNetwork.SendRate);
        //서버 접속
        PhotonNetwork.ConnectUsingSettings();
    }

    //포톤 서버에 접속후 호출되는 콜백 함수
    public override void OnConnectedToMaster(){
        Debug.Log("Connected to Master!!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby(); //로비 입장
    }
    //로비에 접속 후 호출되는 콜백 함수
    public override void OnJoinedLobby(){
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinRandomRoom(); // 랜덤 매치메이킹 기능 제공

    }

    //랜덤한 룸입장이 실패했을 경우 호출되는 콜백 함수
    public override void OnJoinRandomFailed(short returnCode, string message){
        Debug.Log($"JoinRandom Filed{returnCode}:{message}");

        //룸의 속성 정의
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;//최대 접속자수
        ro.IsOpen = true;//룸의 오픈여부
        ro.IsVisible = true;//로비에서 룸 목록에 노출 시킬지 여부

        //룸 생성
        PhotonNetwork.CreateRoom("My Room" , ro);

    }
    //룸 생성 후 호출되는 콜백 함수
    public override void OnCreatedRoom(){
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    //룸에 입장한 후 호출되는 콜백 함수
    public override void OnJoinedRoom(){
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        //룸에 접속한 사용자 정보 확인
        foreach(var player in PhotonNetwork.CurrentRoom.Players){
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}");
            playerNum = PhotonNetwork.LocalPlayer.ActorNumber;
            //$ => String.Format() $""
        }

        ////캐릭터 생성
        
        PhotonNetwork.Instantiate("MiniGamePlayer_0"+Random.RandomRange(0,0), pos[Random.RandomRange(0,pos.Length)].position,Quaternion.identity);
        

    }
    public void GoToTItle()
    {
        SceneManager.LoadScene("StartScene");
    }
}
