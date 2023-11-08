using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtons : MonoBehaviour
{
    [SerializeField] Button clientButton;
    [SerializeField] Button serverButton;
    [SerializeField] Button hostButton;

    private void Awake()
    {
        clientButton.onClick.AddListener(OnClick_ClientBtn);
        serverButton.onClick.AddListener(OnClick_ServerBtn);
        hostButton.onClick.AddListener(OnClick_HostBtn);
    }

    public void OnClick_ClientBtn()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void OnClick_ServerBtn()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void OnClick_HostBtn()
    {
        NetworkManager.Singleton.StartHost();
    }
}
