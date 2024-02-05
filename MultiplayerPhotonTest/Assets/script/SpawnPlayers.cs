using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] Transform [] SpawnPoints;

    public GameObject playerPrefab;

    private void Start()
    {
        Vector3 randomPosition  = new Vector3(SpawnPoints[Random.Range(0, SpawnPoints.Length)].position.x, 
            SpawnPoints[Random.Range(0, SpawnPoints.Length)].position.y, 
            SpawnPoints[Random.Range(0, SpawnPoints.Length)].position.z);

        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }
}
