using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        //캐릭터 출현 정보를 배열에 저장
        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = PhotonNetwork.LocalPlayer.ActorNumber;
        
        //캐릭터 생성
        PhotonNetwork.Instantiate(playerPrefab.name ,points[idx].position,points[idx].rotation,0);
    }
}