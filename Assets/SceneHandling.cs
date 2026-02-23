using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void ChangeScene(string sceneNameToLoad)
    {
        if (sceneNameToLoad == "Demarrage")
        {
            foreach (var objects in GameObject.FindGameObjectsWithTag("LogiqueJeu"))
            {
                Destroy(objects);
            }
        }

        Console.WriteLine("On est là même si macron le veut pas nous on est là");
        SceneManager.LoadScene(sceneNameToLoad);
    }


}
