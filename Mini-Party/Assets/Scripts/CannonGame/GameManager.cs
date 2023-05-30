using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class GameManager : MonoBehaviourPunCallbacks
{
    private Dictionary<int, int> playerRanks = new Dictionary<int, int>();
    private int totalPlayers;

    private void Start()
    {
        totalPlayers = PhotonNetwork.PlayerList.Length;

        // 모든 플레이어의 등수를 1로 초기화
        foreach (var player in PhotonNetwork.PlayerList)
        {
            playerRanks.Add(player.ActorNumber, 1);
        }
    }

    private void Update()
    {
        // 게임 종료 조건 체크
        if (totalPlayers > 1)
        {
            int remainingPlayers = GetRemainingPlayersCount();

            if (remainingPlayers == 1)
            {
                // 한 명의 플레이어만 남았으므로 게임 종료
                // 게임 종료 처리 로직을 여기에 구현
            }
        }
    }

    private int GetRemainingPlayersCount()
    {
        int count = 0;

        // 등수가 1인 플레이어 수 체크
        foreach (var playerRank in playerRanks)
        {
            if (playerRank.Value == 1)
            {
                count++;
            }
        }

        return count;
    }

    private void PlayerDropped(int playerId)
    {
        // 플레이어가 떨어졌을 때 등수 변경 처리
        int maxRank = GetMaxRank();

        if (playerRanks.ContainsKey(playerId))
        {
            // 떨어진 플레이어의 등수를 가장 높은 등수 + 1로 설정
            playerRanks[playerId] = maxRank + 1;

            // 등수 변경을 모든 클라이언트에 동기화
            photonView.RPC("UpdatePlayerRank", RpcTarget.All, playerId, playerRanks[playerId]);
        }
    }

    private int GetMaxRank()
    {
        int maxRank = 0;

        // 가장 높은 등수 찾기
        foreach (var playerRank in playerRanks)
        {
            if (playerRank.Value > maxRank)
            {
                maxRank = playerRank.Value;
            }
        }

        return maxRank;
    }

    [PunRPC]
    private void UpdatePlayerRank(int playerId, int rank)
    {
        // 등수 변경을 받은 클라이언트에서 실행되는 로직
        // 등수를 업데이트하고 UI 등을 갱신하는 등의 작업을 여기에 구현
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // 플레이어가 방을 나갔을 때 처리
        int playerId = otherPlayer.ActorNumber;

        // 플레이어가 나갔으므로 등수를 -1로 설정
        playerRanks[playerId] = -1;

        // 플레이어 등수 변경 처리
        PlayerDropped(playerId);
    }
}
