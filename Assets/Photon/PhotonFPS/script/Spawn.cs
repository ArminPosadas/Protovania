using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    public Canvas mainCanvas;
    private static string selectedPrefab;

    public void OnButtonClic(string buttonType)
    {
        Debug.Log("entro al boton");
        switch (buttonType)
        {
            case "clasemage":
                HandleClassSelection();
                selectedPrefab = "Mage";
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", selectedPrefab), new Vector3(4, 80.81f, -84.5f), Quaternion.identity, 0);
                break;
            case "claserogue":
                HandleClassSelection();
                selectedPrefab = "Rogue";
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", selectedPrefab), new Vector3(4, 80.81f, -84.5f), Quaternion.identity, 0);
                break;
            case "claseengineer":
                HandleClassSelection();
                selectedPrefab = "Engineer";
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", selectedPrefab), new Vector3(4, 80.81f, -84.5f), Quaternion.identity, 0);
                break;
            case "clasemelee":
                HandleClassSelection();
                selectedPrefab = "Player2";
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", selectedPrefab), new Vector3(4, 80.81f, -84.5f), Quaternion.identity, 0);
                break;
            case "exit":
                HandleExit();
                break;
            default:
                Debug.Log("no pasa nada");
                break;
        }
    }

    private void HandleClassSelection()
    {
        if (mainCanvas != null)
        {
            mainCanvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("no hay canvas");
        }
    }

    private void HandleExit()
    {
        SceneTransitionManager sceneManager = FindObjectOfType<SceneTransitionManager>();
        if (sceneManager != null)
        {
            sceneManager.ChangeScene("Main"); 
        }
        else
        {
            Debug.LogError("SceneTransitionManager no encontrado en la escena.");
        }
    }

    public static string GetSelectedPrefab()
    {
        return selectedPrefab;
    }
}




