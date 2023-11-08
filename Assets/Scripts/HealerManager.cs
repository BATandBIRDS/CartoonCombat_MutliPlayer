using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HealerManager : NetworkBehaviour
{
    [SerializeField] Transform spawnPoint;
    void Awake()
    {
        transform.position = spawnPoint.position;
    }
}
