using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class CamManager : NetworkBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public float maxZoomDistance = 10.0f; // Adjust as needed

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            // On the server, handle camera and player setup.
            SetupCameraTargetGroup();
        }
    }

    private void SetupCameraTargetGroup()
    {
        var connectedClients = NetworkManager.Singleton.ConnectedClientsList;
        foreach (var client in connectedClients)
        {
            // Find the player's GameObject or NetworkObject
            // and add it to the Cinemachine Target Group.
            NetworkObject playerObject = client.PlayerObject;
            if (playerObject != null)
            {
                targetGroup.AddMember(playerObject.transform, 1.0f, maxZoomDistance);
                //CinemachineVirtualCamera vc = GetComponent<CinemachineVirtualCamera>();
                //vc.Follow = targetGroup.;
            }
        }
    }
}
