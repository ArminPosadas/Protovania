using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    public Canvas mainCanvas;

    public void OnButtonClic(string buttonType)
    {
    Debug.Log("entro al boton");
        switch (buttonType)
        {
            case "clasemage":
                HandleClassSelection();
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Mage"), new Vector3(4, 80.81f, -84.5f), Quaternion.identity, 0);
                break;
            case "claserogue":
                HandleClassSelection();
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Rogue"), new Vector3(4, 80.81f, -84.5f), Quaternion.identity, 0);
                break;
            case "claseengineer":
                HandleClassSelection();
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Engineer"), new Vector3(4, 80.81f, -84.5f), Quaternion.identity, 0);
                break;
            case "clasemelee":
                HandleClassSelection();
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player2"), new Vector3(4, 80.81f, -84.5f), Quaternion.identity, 0);
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
       
        SceneManager.LoadScene("Main");
    }
}
