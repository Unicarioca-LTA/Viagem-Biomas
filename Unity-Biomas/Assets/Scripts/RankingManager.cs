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

    [Header("Configuração do Pop-up de Confirmação")]
    public GameObject painelConfirmacao; 

    [Header("Configuração do Pop-up de Nome Repetido")]
    public GameObject painelJogadorRepetido; // 👈 NOVO: Arraste o seu novo painel aqui no Inspector

    void Awake()
    {
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
        SincronizarJogadorAtual();

        if (textoRanking != null)
        {
            // Forçamos a atualização visual logo no início
            MostrarRanking();
        }
    }

    void SincronizarJogadorAtual()
    {
        string nome = "Jogador";
        if (GameHandler.instance != null && !string.IsNullOrEmpty(GameHandler.instance.nomeJogador))
        {
            nome = GameHandler.instance.nomeJogador.Trim();
        }

        string chaveSalvamento = "Pontos_" + nome.ToLower();

        if (PlayerPrefs.HasKey(chaveSalvamento))
        {
            int pontuacaoTotalAtual = PlayerPrefs.GetInt(chaveSalvamento);
            
            // CORREÇÃO: Agora nós atualizamos os pontos direto, sem bloquear o jogador atual!
            SalvarPontuacao(nome, pontuacaoTotalAtual);
        }
    }

    // Mantemos a função caso você precise dela para outra checagem, mas ela não bloqueia mais o fluxo acima
    private bool JogadorJaExisteNoRanking(string nome)
    {
        if (!PlayerPrefs.HasKey("Ranking")) return false;

        string json = PlayerPrefs.GetString("Ranking");
        Ranking rankingSalvo = JsonUtility.FromJson<Ranking>(json);
        
        if (rankingSalvo != null && rankingSalvo.jogadores != null)
        {
            return rankingSalvo.jogadores.Exists(j => j.nome.ToLower() == nome.ToLower());
        }
        return false;
    }

    public void SalvarPontuacao(string nome, int pontuacao)
    {
        Ranking ranking = new Ranking();

        if (PlayerPrefs.HasKey("Ranking"))
        {
            string json = PlayerPrefs.GetString("Ranking");
            Ranking rankingSalvo = JsonUtility.FromJson<Ranking>(json);
            if (rankingSalvo != null)
            {
                ranking = rankingSalvo;
            }
        }

        Jogador jogadorExistente = ranking.jogadores.Find(j => j.nome.ToLower() == nome.ToLower());

        if (jogadorExistente != null)
        {
            jogadorExistente.pontuacao = pontuacao;
            Debug.Log($"Usuário existente encontrado. Pontuação de {nome} atualizada para: {pontuacao}");
        }
        else
        {
            ranking.jogadores.Add(new Jogador(nome, pontuacao));
            Debug.Log($"Novo usuário registrado: {nome} com {pontuacao} pts");
        }

        ranking.jogadores.Sort((a, b) => b.pontuacao.CompareTo(a.pontuacao));

        if (ranking.jogadores.Count > 10)
        {
            ranking.jogadores.RemoveRange(10, ranking.jogadores.Count - 10);
        }

        string novoJson = JsonUtility.ToJson(ranking);
        PlayerPrefs.SetString("Ranking", novoJson);
        PlayerPrefs.Save();
    }

    public void MostrarRanking()
    {
        if (textoRanking == null) return;

        if (!PlayerPrefs.HasKey("Ranking"))
        {
            textoRanking.alignment = TextAlignmentOptions.Center;
            textoRanking.text = "\n";
            return;
        }

        string json = PlayerPrefs.GetString("Ranking");
        Ranking ranking = JsonUtility.FromJson<Ranking>(json);

        if (ranking == null || ranking.jogadores == null || ranking.jogadores.Count == 0)
        {
            textoRanking.alignment = TextAlignmentOptions.Center;
            textoRanking.text = "\n";
            return;
        }

        textoRanking.alignment = TextAlignmentOptions.TopLeft;
        string texto = "\n\n";

        int totalJogadores = ranking.jogadores.Count;
        int limiteCaracteres = 10; 

        for (int i = 0; i < 5; i++)
        {
            if (i < totalJogadores)
            {
                string nomeTruncadoEsq = TruncarNome(ranking.jogadores[i].nome, limiteCaracteres);
                texto += "<space=200px>" + (i + 1) + "º - " + nomeTruncadoEsq + " : " + ranking.jogadores[i].pontuacao + " pts";
            }

            int indiceDireita = i + 5;
            if (indiceDireita < totalJogadores)
            {
                string nomeTruncadoDir = TruncarNome(ranking.jogadores[indiceDireita].nome, limiteCaracteres);
                texto += "<pos=55%>" + (indiceDireita + 1) + "º - " + nomeTruncadoDir + " : " + ranking.jogadores[indiceDireita].pontuacao + " pts";
            }
            texto += "\n";
        }

        textoRanking.text = texto;
    }

    private string TruncarNome(string nomeOriginal, int maxCaracteres)
    {
        if (string.IsNullOrEmpty(nomeOriginal)) return "";
        if (nomeOriginal.Length > maxCaracteres)
        {
            return nomeOriginal.Substring(0, maxCaracteres) + "...";
        }
        return nomeOriginal;
    }

    public void AbrirJanelaConfirmacao() { if (painelConfirmacao != null) painelConfirmacao.SetActive(true); }
    public void CancelarReiniciar() { if (painelConfirmacao != null) painelConfirmacao.SetActive(false); }

    // ========================================================
    // CONTROLES DO NOVO PAINEL DE JOGADOR REPETIDO
    // ========================================================
    public void AbrirPainelJogadorRepetido() 
    { 
        if (painelJogadorRepetido != null) painelJogadorRepetido.SetActive(true); 
    }

    public void FecharPainelJogadorRepetido() 
    { 
        if (painelJogadorRepetido != null) painelJogadorRepetido.SetActive(false); 
        
        // Opcional: Se quiser redirecionar o usuário de volta para a tela de input de nome ao fechar,
        // você pode colocar a lógica ou carregamento de cena aqui.
    }

    public void ConfirmarEReiniciarRanking()
    {
        if (PlayerPrefs.HasKey("Ranking"))
        {
            try
            {
                string json = PlayerPrefs.GetString("Ranking");
                Ranking rankingSalvo = JsonUtility.FromJson<Ranking>(json);

                if (rankingSalvo != null && rankingSalvo.jogadores != null)
                {
                    foreach (Jogador j in rankingSalvo.jogadores)
                    {
                        string chavePontosDoJogador = "Pontos_" + j.nome.Trim().ToLower();
                        
                        if (PlayerPrefs.HasKey(chavePontosDoJogador))
                        {
                            PlayerPrefs.DeleteKey(chavePontosDoJogador);
                            Debug.Log($"Dados locais purgados para: {chavePontosDoJogador}");
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Erro ao purgar dados individuais dos jogadores: " + e.Message);
            }

            PlayerPrefs.DeleteKey("Ranking");
        }

        if (GameHandler.instance != null)
        {
            string nomeAtual = string.IsNullOrEmpty(GameHandler.instance.nomeJogador) ? "Jogador" : GameHandler.instance.nomeJogador.Trim();
            string chaveSessaoAtual = "Pontos_" + nomeAtual.ToLower();
            if (PlayerPrefs.HasKey(chaveSessaoAtual))
            {
                PlayerPrefs.DeleteKey(chaveSessaoAtual);
            }
        }
        
        PlayerPrefs.Save();

        if (painelConfirmacao != null) painelConfirmacao.SetActive(false);
        MostrarRanking();
    }
}