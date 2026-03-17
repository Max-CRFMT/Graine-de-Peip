using UnityEngine;

public class viewSwitcherScript : MonoBehaviour
{
    public void switchview()
    {
        GameLogic.instance.MoveCamera();
    }
}
