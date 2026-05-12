using UnityEngine;

public class RecupRetourMenu : MonoBehaviour
{
    public void RetourLobby()
    {
        GameLogic.instance.RebootGame();
    }
}
