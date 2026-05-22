using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppelMethodes : MonoBehaviour
{
    private Scene curScene;
    private GameObject[] obj;
    private Canvas c;
    public string biome;
    private TMPro.TMP_Dropdown[] droplist;
    private TMPro.TMP_Dropdown drop;

    public void Amelioration_du_jardin(string actionType)
    {
        curScene = SceneManager.GetActiveScene();
        obj = curScene.GetRootGameObjects();
        if (actionType == "niveau")
        {
            TurnHandler.instance.PlayerActuel.continent.jardin.Amelioration_niveau_jardin();
            foreach (GameObject o in obj)
            {
                if (o.name == "PopUpAmeliorationJardin")
                {
                    o.SetActive(false);
                }
            }
        }
        else
        {   
            c = MenuOptions.instance.ResearchCanvasSelonTag("canvasBiome");
            c.gameObject.SetActive(true);
            c = MenuOptions.instance.ResearchCanvasSelonTag("canvasChoixAmelioration");
            c.gameObject.SetActive(false);
        }
    }

    public void Amelioration_biome()
    {
        Debug.Log(TurnHandler.instance.PlayerActuel.pseudo);
        c = MenuOptions.instance.ResearchCanvasSelonTag("canvasBiome");
        droplist = c.gameObject.GetComponentsInChildren<TMPro.TMP_Dropdown>();
        drop = droplist[0];
        biome = drop.options[drop.value].text;
        if (TurnHandler.instance.PlayerActuel.continent.jardin.liste_biome_jardin.Contains(biome))
        {
            Debug.Log("Le jardin contient déjà l'amélioration pour ce biome.");
        }
        else
        {
            TurnHandler.instance.PlayerActuel.continent.jardin.Ajout_biome_jardin(biome);
            c.gameObject.SetActive(false);
            curScene = SceneManager.GetActiveScene();
            obj = curScene.GetRootGameObjects();
            curScene = SceneManager.GetActiveScene();
            obj = curScene.GetRootGameObjects();
            foreach (GameObject o in obj)
            {
                if (o.name == "PopUpAmeliorationJardin")
                {
                    o.SetActive(false);
                }
            }
            c = MenuOptions.instance.ResearchCanvasSelonTag("canvasChoixAmelioration");
            c.gameObject.SetActive(true);
        }
    }
}
