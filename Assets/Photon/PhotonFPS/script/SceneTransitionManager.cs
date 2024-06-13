using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;

public class SceneTransitionManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PhotonNetwork.InRoom)
        {
            string selectedPrefab = Spawn.GetSelectedPrefab();
            if (!string.IsNullOrEmpty(selectedPrefab))
            {
                // Aquí puedes ajustar la posición de spawn en la nueva escena
                Vector3 spawnPosition = new Vector3(0, 1, 0); // Cambia esto según la lógica de tu juego
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", selectedPrefab), spawnPosition, Quaternion.identity, 0);
            }
        }
    }
    public class SceneChanger : MonoBehaviour
    {
        public int nextScene = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
