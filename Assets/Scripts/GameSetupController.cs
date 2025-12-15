using Photon.Pun;
using UnityEngine;
using System.Collections;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    [Header("Configuración")]
    [Tooltip("El nombre EXACTO del prefab en la carpeta Resources")]
    [SerializeField] private string playerPrefabName = "Player";

    private void Start()
    {
        // VERIFICACIÓN DE SEGURIDAD
        // Si intentas dar Play directo a esta escena sin pasar por el Menú, esto evitará errores.
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Scene Cargada. Instanciando Jugador...");
            SpawnPlayer();
        }
        else
        {
            Debug.LogError("Esperardo coneccion a la Sala...");
            // Opcional: Podrías devolverlo al menú automáticamente aquí
        }
    //     // VERIFICACIÓN DE SEGURIDAD
    //     // Si intentas dar Play directo a esta escena sin pasar por el Menú, esto evitará errores.
    //     if (PhotonNetwork.IsConnectedAndReady)
    //     {
    //         Debug.Log("Scene Cargada. Instanciando Jugador...");
    //         SpawnPlayer();
    //         StartCoroutine(PingLoop());
    //     }
    //     else
    //     {
    //         Debug.LogError("¡Error! No estás conectado a Photon. Inicia el juego desde la escena del Menú.");
    //         // Opcional: Podrías devolverlo al menú automáticamente aquí
    //         UnityEngine.SceneManagement.SceneManager.LoadScene("02_GameScene");
    //     }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Scene Cargada. Instanciando Jugador...");
        SpawnPlayer();
        StartCoroutine(PingLoop());
    }

    private void SpawnPlayer()
    {
        // Generamos una posición aleatoria para evitar que todos nazcan en el punto (0,0,0)
        // Puedes cambiar esto por puntos de spawn fijos (SpawnPoints) más adelante.
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));

        // IMPORTANTE: PhotonNetwork.Instantiate requiere que el objeto esté en una carpeta llamada "Resources"
        // El string debe coincidir exactamente con el nombre del archivo prefab.
        // devuelve la referencia al GameObject instanciado
        GameObject player = PhotonNetwork.Instantiate(playerPrefabName, randomPosition, Quaternion.identity);

        // guarda referencia en el objeto Player local para acceso global
        PhotonNetwork.LocalPlayer.TagObject = player;
    }

    private IEnumerator PingLoop()
    {
        // Este bucle se ejecuta mientras sigamos conectados y en la sala
        while (PhotonNetwork.InRoom)
        {
            yield return new WaitForSeconds(2f); // Actualiza cada 2 segundos para no saturar la consola
            Debug.Log($"Ping: {PhotonNetwork.GetPing()} ms");
        }
    }
}