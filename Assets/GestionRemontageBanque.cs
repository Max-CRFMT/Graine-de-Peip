using System.Linq;
using UnityEngine;

public class GestionRemontageBanque : MonoBehaviour
{
    public int nom;
    int indexCarte;
    bool contientCarte = false;
    Player aLaCarte;

    public void SurClique()
    {
        if (TurnHandler.instance.PlayerActuel.restaurationEnCours)
        {
            Carte carteSelect = TurnHandler.instance.PlayerActuel.continent.banque.listeDeListes[TurnHandler.instance.PlayerActuel.curBank][nom];
            int prixAction = 3;
            if (carteSelect.continent_name == TurnHandler.instance.PlayerActuel.continent.name)
            {
                prixAction = 2;
            }
            if (TurnHandler.instance.PlayerActuel.VerifMontant(prixAction))
            {
                contientCarte = false;
                foreach (Player p in GameObject.Find("GameLogic").GetComponent<GameLogic>().Liste_Joueurs)
                {
                    for (int i = 0; i < p.continent.defausse.Count(); i++)
                    {
                        if (p.continent.defausse[i].nom == carteSelect.nom)
                        {
                            contientCarte = true;
                            aLaCarte = p;
                            indexCarte = i;
                        }
                    }
                }
                if (contientCarte)
                {
                    GameObject.Find(GameObject.Find("GameLogic").GetComponent<GameLogic>().Dico_NomCarte_Attache[aLaCarte.continent.defausse[indexCarte].nom]).GetComponent<SpeciesStackScript>().Ajouter(aLaCarte.continent.defausse[indexCarte]);
                    GameObject.Find(GameObject.Find("GameLogic").GetComponent<GameLogic>().Dico_NomCarte_Attache[aLaCarte.continent.defausse[indexCarte].nom]).GetComponent<SpeciesStackScript>().IncreaseCardAmount();
                    aLaCarte.continent.defausse.RemoveAt(indexCarte);
                    Debug.Log("Carte restauree !");
                }
                else
                {
                    Debug.Log("Y a pas ce qui faut dans la defausse");
                }
            }
            else
            {
                Debug.Log("Montant non acquis, action annulée, cheh");
            }
            TurnHandler.instance.PlayerActuel.FinRestauration();
        }
        else
        {
            GestionPostRecolte.instance.PiocheOuBanque = 'B';
            GestionPostRecolte.instance.carte_cible_recolte = TurnHandler.instance.PlayerActuel.continent.banque.listeDeListes[GestionPostRecolte.instance.banque2ou3][nom];
            MenuOptions.instance.ResearchCanvasSelonTag("BanqueList").gameObject.SetActive(false);
            TurnHandler.instance.PlayerActuel.continent.banque.Banque1.transform.GetChild(0).gameObject.SetActive(true);
            TurnHandler.instance.PlayerActuel.continent.banque.Banque2.transform.GetChild(0).gameObject.SetActive(false);
            TurnHandler.instance.PlayerActuel.continent.banque.Banque3.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
