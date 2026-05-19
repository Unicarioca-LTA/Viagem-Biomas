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
    public static RankingManager instance;

    public TextMeshProUGUI textoRanking;

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
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

    // =========================
    // SALVAR PONTUAÇÃO
    // =========================

    public void SalvarPontuacao(int pontuacao)
    {
        // Nome padrão
        string nome = "Jogador";

        // Pega nome do jogador se existir
        if (GameHandler.instance != null)
        {
            nome = GameHandler.instance.nomeJogador;
        }

        Ranking ranking = new Ranking();

        // Carrega ranking salvo
        if (PlayerPrefs.HasKey("Ranking"))
        {
            string json = PlayerPrefs.GetString("Ranking");

            Ranking rankingSalvo = JsonUtility.FromJson<Ranking>(json);

            if (rankingSalvo != null)
            {
                ranking = rankingSalvo;
            }
        }

        // Adiciona novo jogador
        ranking.jogadores.Add(new Jogador(nome, pontuacao));

        // Ordena do maior para o menor
        ranking.jogadores.Sort((a, b) => b.pontuacao.CompareTo(a.pontuacao));

        // Mantém apenas TOP 10
        if (ranking.jogadores.Count > 10)
        {
            ranking.jogadores.RemoveRange(10, ranking.jogadores.Count - 10);
        }

        // Salva em JSON
        string novoJson = JsonUtility.ToJson(ranking);

        PlayerPrefs.SetString("Ranking", novoJson);
        PlayerPrefs.Save();

        Debug.Log("Pontuação salva: " + nome + " - " + pontuacao);
    }

    // =========================
    // MOSTRAR RANKING
    // =========================

    public void MostrarRanking()
    {
        if (textoRanking == null)
        {
            Debug.LogError("TextoRanking não foi conectado no Inspector!");
            return;
        }

        // Se não existir ranking salvo
        if (!PlayerPrefs.HasKey("Ranking"))
        {
            textoRanking.text = "RANKING VAZIO";
            return;
        }

        string json = PlayerPrefs.GetString("Ranking");

        Ranking ranking = JsonUtility.FromJson<Ranking>(json);

        // Proteção contra erro
        if (ranking == null || ranking.jogadores == null)
        {
            textoRanking.text = "RANKING VAZIO";
            return;
        }

        // Se lista estiver vazia
        if (ranking.jogadores.Count == 0)
        {
            textoRanking.text = "RANKING VAZIO";
            return;
        }

        // Monta texto
        string texto = "RANKING\n\n";

        for (int i = 0; i < ranking.jogadores.Count; i++)
        {
            texto += (i + 1) + "º - " +
                     ranking.jogadores[i].nome +
                     " : " +
                     ranking.jogadores[i].pontuacao +
                     " pts\n";
        }

        textoRanking.text = texto;
    }
}