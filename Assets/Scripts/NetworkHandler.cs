using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkHandler : NetworkBehaviour
{
    public List<GameObject> otherFellas;
    public List<Transform> spawnPoints;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer) // Check if this code is running on the server
        {
            GameObject[] go = new GameObject[otherFellas.Count];
            NetworkObject[] spawnObjects = new NetworkObject[otherFellas.Count];

            for (int i = 0; i < otherFellas.Count; i++)
            {
                go[i] = Instantiate(otherFellas[i], spawnPoints[i]);
                spawnObjects[i] = go[i].GetComponent<NetworkObject>();
                spawnObjects[i].Spawn();
            }
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }
}
