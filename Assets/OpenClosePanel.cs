using UnityEngine;

public class OpenClosePanel : MonoBehaviour 
{
    public GameObject panel;

    public void openPanel()
    {
        panel.SetActive(true);
    }
    public void closePanel()
    {
        panel.SetActive(false);
    }
}
