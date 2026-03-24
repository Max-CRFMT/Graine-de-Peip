using System.Threading;
using TMPro;
using UnityEngine;

public class TempsDiscution : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timertext;
    [SerializeField] float tempsrestant;


    public bool BoutonFinDiscutionPressed = false;

    private void Awake()
    {
         GameObject textobj = GameObject.FindGameObjectWithTag("TexteTimer");
         timertext = textobj.GetComponent<TextMeshProUGUI>();
    }

    private void SuprressionTimers()
    {
        GameLogic.instance.SupprimerGameObjectSelonTag("TimerDIscution");
        GameLogic.instance.SupprimerGameObjectSelonTag("TexteTimer");
    }

    public void FinDiscution()
    {
        SuprressionTimers();
        TurnHandler.instance.FinDiscution = true;
    }

    void Update()
    {
        if (!MenuInGame.instance.MenuEnCours)
        {
            if (BoutonFinDiscutionPressed || tempsrestant <= 0)
            {
                FinDiscution();
                return;
            }

            if (tempsrestant > 0)
            {
                tempsrestant -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(tempsrestant / 60);
                int secondes = Mathf.FloorToInt(tempsrestant % 60);
                timertext.text = string.Format("{0:00}:{1:00}", minutes, secondes);
            }
        }

    }

    public void BoutonPressed()
    {
        BoutonFinDiscutionPressed = true;
    }
}
