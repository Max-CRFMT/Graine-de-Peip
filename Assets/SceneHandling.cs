using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public static SceneHandler instance ;

    void Awake()
    {
        instance = this;
    }
    public void ChangeScene(string sceneNameToLoad)
    {
        if ((sceneNameToLoad == "Demarrage") || (sceneNameToLoad == "Lobby"))
        {
            foreach (var objects in GameObject.FindGameObjectsWithTag("LogiqueJeu"))
            {
                Destroy(objects);
            }
        }

        if (sceneNameToLoad == "Lobby")
        {
            foreach (var objects in GameObject.FindGameObjectsWithTag("Options"))
            {
                Destroy(objects);
            }
        }


        Console.WriteLine("On est l� m�me si macron le veut pas nous on est l�");
        SceneManager.LoadScene(sceneNameToLoad);
    }


}
