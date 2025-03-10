using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    static NetworkManager networkManager;
    static UnityTransport networkTransport;

    static string connectionIP = "127.0.0.1";

    void Awake()
    {
        networkManager = GetComponent<NetworkManager>();
        networkTransport = (UnityTransport)GetComponent<NetworkTransport>();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 600));

        if (!networkManager.IsClient && !networkManager.IsServer)
            StartButtons();
        else
            StatusLabels();
            //SubmitNewPosition();

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        if (GUILayout.Button("Host")) networkManager.StartHost();
        if (GUILayout.Button("Join (Enter IP)")) networkManager.StartClient();

        connectionIP = GUILayout.TextField(connectionIP);
        networkTransport.ConnectionData.Address = connectionIP;

        //if (GUILayout.Button("Server")) networkManager.StartServer();
    }

    static void StatusLabels()
    {
        var mode = networkManager.IsHost ?
            "Host" : networkManager.IsServer ? "Server" : "Client";

        /*GUILayout.Label("Transport: " +
            networkManager.NetworkConfig.NetworkTransport.GetType().Name);*/
        GUILayout.Label("Mode: " + mode);
    }

    /*
    static void SubmitNewPosition()
    {
        if (GUILayout.Button(networkManager.IsServer ? "Move" : "Request Position Change"))
        {
            if (networkManager.IsServer && !networkManager.IsClient)
            {
                foreach (ulong uid in networkManager.ConnectedClientsIds)
                    networkManager.SpawnManager.GetPlayerNetworkObject(uid).GetComponent<NetPlayer>().Move();
            }
            else
            {
                var playerObject = networkManager.SpawnManager.GetLocalPlayerObject();
                var player = playerObject.GetComponent<NetPlayer>();
                player.Move();
            }
        }
    }*/
}