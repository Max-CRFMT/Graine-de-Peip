using UnityEngine;

public class QuitterJeu : MonoBehaviour
{
    public void Quitter()
    {
        Debug.Log("Ouais c'est actif");
        Application.Quit();
    }
}
