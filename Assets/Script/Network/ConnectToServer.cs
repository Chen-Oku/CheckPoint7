using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.InputSystem.Interactions;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button startButton;

    private void Awake()
    {
        startButton.interactable = false;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

        startButton.onClick.AddListener(ConnectToLobby);
            
    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        startButton.interactable = true;
    }

    private void ConnectToLobby()
    {
        startButton.interactable = false;

        PhotonNetwork.JoinLobby();

    }

}

// #if UNITY_EDITOR
//      print("Connected to Lobby");
//     #endif 