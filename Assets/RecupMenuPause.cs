using UnityEngine;
using UnityEngine.SceneManagement;
public class RecupMenuPause : MonoBehaviour
{
    public void Reprendre()
    {
        MenuInGame.instance.MenuEnCours = false;
        MenuInGame.instance.EnleverMenuIngame();
    }

    public void QuitterPartie()
    {
        SceneHandler.instance.ChangeScene("Lobby");
    }

    public void Options()
    {
        MenuOptions.instance.AfficherMenuOptions();
    }
}
