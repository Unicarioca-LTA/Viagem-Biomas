using UnityEngine;
using TMPro;

public class TelaRanking : MonoBehaviour
{
    public TextMeshProUGUI textoRanking;

    void Start()
    {
        RankingManager.instance.textoRanking = textoRanking;
        RankingManager.instance.MostrarRanking();
    }
}
