using UnityEngine;

public class Jardin
{
    public string name;
    public int limite_max_jardin = 8;
    public static int niveau_jardin = 0;
    public static int fibo1 = 0;
    public static int fibo2 = 1;
    public Jardin(string nom_continent)
    {
        name = nom_continent;
    }

    public void Amelioration_du_jardin()
    {
        if (niveau_jardin < limite_max_jardin)
        {
            niveau_jardin = fibo1 + fibo2;
            fibo1 = fibo2;
            fibo2 = niveau_jardin;
            Debug.Log(niveau_jardin);
        }
        else
        {
            Debug.Log("Niveau maximal atteint");
        }
    }
}
