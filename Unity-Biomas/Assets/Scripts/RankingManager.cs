using UnityEngine;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class Jogador
{
    public string nome;
    public int pontuacao;

    public Jogador(string nome, int pontuacao)
    {
        this.nome = nome;
        this.pontuacao = pontuacao;
    }
}

[System.Serializable]
public class Ranking
{
    public List<Jogador> jogadores = new List<Jogador>();
}

public class RankingManager : MonoBehaviour
{
    public static RankingManager instance; // 👈 ESSA LINHA FALTAVA

    public TextMeshProUGUI textoRanking;

    void Awake()
    {
        // 👇 ISSO RESOLVE O ERRO DO INSTANCE
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // mantém entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (textoRanking != null)
        {
            MostrarRanking();
        }
    }

    public void SalvarPontuacao(int pontuacao)
    {
        string nome = GameHandler.instance.nomeJogador;

        Ranking ranking = new Ranking();

        if (PlayerPrefs.HasKey("Ranking"))
        {
            string json = PlayerPrefs.GetString("Ranking");
            ranking = JsonUtility.FromJson<Ranking>(json);
        }

        ranking.jogadores.Add(new Jogador(nome, pontuacao));

        // Ordena do maior para o menor
        ranking.jogadores.Sort((a, b) => b.pontuacao.CompareTo(a.pontuacao));

        // Limita a 10 jogadores
        if (ranking.jogadores.Count > 10)
        {
            ranking.jogadores.RemoveRange(10, ranking.jogadores.Count - 10);
        }

        string novoJson = JsonUtility.ToJson(ranking);
        PlayerPrefs.SetString("Ranking", novoJson);
        PlayerPrefs.Save();

        Debug.Log("Salvando: " + nome + " - " + pontuacao);
    }

    public void MostrarRanking()
    {

        if (textoRanking == null)
        {
            Debug.LogError("Nenhum TextMeshProUGUI encontrado na cena!");
            return;
        }

        if (!PlayerPrefs.HasKey("Ranking"))
        {
            textoRanking.text = "RANKING";
            return;
        }

        string json = PlayerPrefs.GetString("Ranking");
        Ranking ranking = JsonUtility.FromJson<Ranking>(json);

        string texto = "RANKING:\n\n";

        for (int i = 0; i < ranking.jogadores.Count; i++)
        {
            texto += (i + 1) + ". " +
                     ranking.jogadores[i].nome + " - " +
                     ranking.jogadores[i].pontuacao + " pontos\n";
        }

        textoRanking.text = texto;
    }
}