using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ConnectToLobby : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Awake()
    {
        startButton.interactable = false;

        PhotonNetwork.JoinLobby();
    }

    private void Start()
    {
        startButton.onClick.AddListener(() => print("Joined Lobby"));

    }

    // public override void OnJoinedLobby()
    // {
    //     base.OnJoinedLobby();

    //     startButton.interactable = true;
    // }




}
