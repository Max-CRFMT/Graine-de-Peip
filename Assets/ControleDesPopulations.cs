using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ControleDesPopulations : MonoBehaviour
{
    public Dictionary<string, string> Dico_NomCarte_Attache;
    private Player joueur;
    public static ControleDesPopulations instance;

    public void Awake()
    {
        instance = this;
    }

    public GameObject PanelControle;
    public GameObject bouton_clique;

    public void Controle(string nom_carte)
    {
        joueur = TurnHandler.instance.PlayerActuel;
        bouton_clique = EventSystem.current.currentSelectedGameObject;
        nom_carte = bouton_clique.name;

        Debug.Log(nom_carte);

        GameObject carte_plante = GameObject.Find(nom_carte);

        GameObject enfant_plante = carte_plante.transform.GetChild(0).gameObject;
        TextMeshProUGUI compteur = enfant_plante.GetComponent<TextMeshProUGUI>();
        string nombre_affiche = compteur.text;
        string nom_carte_detache = GameLogic.instance.Dico_NomCarte_Attache.FirstOrDefault(x => x.Value == nom_carte).Key;
        Debug.Log(nom_carte_detache);
        if (int.Parse(nombre_affiche) > 0)
        {
            if (GameLogic.instance.Is_invasif(nom_carte_detache, joueur.continent.defausse))
            {
                MenuOptions.instance.ResearchCanvasSelonTag("TxtControleInvasif").gameObject.SetActive(true);
                Debug.Log("Invasif");
                carte_plante.transform.GetComponent<SpeciesStackScript>().SupprimerCarteControle(1, nom_carte_detache);
            }
            else
            {
                Debug.Log("Else");
                OpenClosePanel.instance.openPanelControl(PanelControle, nom_carte);
            }
        }
    }
}
