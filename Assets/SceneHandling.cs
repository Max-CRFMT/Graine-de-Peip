using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void ChangeScene(string sceneNameToLoad)
    {
        Console.WriteLine("On est là même si macron le veut pas nous on est là");
        SceneManager.LoadScene(sceneNameToLoad);
    }


}
