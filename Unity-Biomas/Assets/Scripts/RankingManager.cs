using UnityEngine;
using TMPro;

public class RankingManager : MonoBehaviour
{
    public TextMeshProUGUI textoRanking;

    void Start()
    {
        int pontuacao = PlayerPrefs.GetInt("Pontuacao", 0);

        textoRanking.text = "Pontuação:\n" + pontuacao;
    }
}

