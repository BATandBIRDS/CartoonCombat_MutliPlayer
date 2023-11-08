using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ChoosenClassInstantiater : NetworkBehaviour
{
    [SerializeField] GameObject choosenPlayerClass;
    //[SerializeField] CinemachineVirtualCamera followCamera;
    //[SerializeField] Camera playerCamera;

    void Awake()
    {
        Instantiate(choosenPlayerClass, transform.position, Quaternion.identity);
        //Instantiate(followCamera, transform.position, Quaternion.identity);
        //Instantiate(playerCamera, transform.position, Quaternion.identity);

        //if (IsServer)
        //{
        //    Instantiate(choosenPlayerClass, transform.position, Quaternion.identity);
        //    Instantiate(followCamera, transform.position, Quaternion.identity);
        //    Instantiate(playerCamera, transform.position, Quaternion.identity);
        //}
        //else if(IsClient)
        //{
        //    InstantiaterServerRpc();
        //}
        

        
    }

    //[ServerRpc]
    //void InstantiaterServerRpc()
    //{
    //    Instantiate(choosenPlayerClass, transform.position, Quaternion.identity);
    //    Instantiate(followCamera, transform.position, Quaternion.identity);
    //    Instantiate(playerCamera, transform.position, Quaternion.identity);
    //}

    
}
