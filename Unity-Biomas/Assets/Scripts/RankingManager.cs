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
        // 🚀 Toda vez que a cena do Ranking abre, buscamos o progresso do jogador atual
        // e atualizamos o ranking de forma definitiva.
        SincronizarJogadorAtual();

        if (textoRanking != null)
        {
            MostrarRanking();
        }
    }

    // ========================================================
    // BUSCA O PROGRESSO ATUAL DO JOGADOR E ENVIA AO RANKING
    // ========================================================
void SincronizarJogadorAtual()
    {
        string nome = "Jogador";
        if (GameHandler.instance != null && !string.IsNullOrEmpty(GameHandler.instance.nomeJogador))
        {
            nome = GameHandler.instance.nomeJogador.Trim();
        }

        // 💡 PADRONIZAÇÃO: Forçamos a chave a ser sempre em minúsculo para evitar conflito de caixa
        string chaveSalvamento = "Pontos_" + nome.ToLower();

        if (PlayerPrefs.HasKey(chaveSalvamento))
        {
            int pontuacaoTotalAtual = PlayerPrefs.GetInt(chaveSalvamento);
            SalvarPontuacao(nome, pontuacaoTotalAtual); // Mantém o nome original com maiúsculas para exibição visual
        }
    }

    // ========================================================
    // SALVAR/ATUALIZAR PONTUAÇÃO (Evita duplicar o mesmo nome)
    // ========================================================
    public void SalvarPontuacao(string nome, int pontuacao)
    {
        Ranking ranking = new Ranking();

        // 1. Carrega o Ranking existente
        if (PlayerPrefs.HasKey("Ranking"))
        {
            string json = PlayerPrefs.GetString("Ranking");
            Ranking rankingSalvo = JsonUtility.FromJson<Ranking>(json);
            if (rankingSalvo != null)
            {
                ranking = rankingSalvo;
            }
        }

        // 2. 🔍 VERIFICAÇÃO CHAVE: O jogador já está no ranking?
        Jogador jogadorExistente = ranking.jogadores.Find(j => j.nome.ToLower() == nome.ToLower());

        if (jogadorExistente != null)
        {
            // Se já existe, apenas atualiza os pontos dele com o novo total acumulado
            jogadorExistente.pontuacao = pontuacao;
            Debug.Log($"Usuário existente encontrado. Pontuação de {nome} atualizada para: {pontuacao}");
        }
        else
        {
            // Se for um jogador inédito, adiciona ele na lista
            ranking.jogadores.Add(new Jogador(nome, pontuacao));
            Debug.Log($"Novo usuário registrado: {nome} com {pontuacao} pts");
        }

        // 3. Ordena do maior para o menor
        ranking.jogadores.Sort((a, b) => b.pontuacao.CompareTo(a.pontuacao));

        // 4. Mantém apenas TOP 10
        if (ranking.jogadores.Count > 10)
        {
            ranking.jogadores.RemoveRange(10, ranking.jogadores.Count - 10);
        }

        // 5. Salva de volta no PlayerPrefs
        string novoJson = JsonUtility.ToJson(ranking);
        PlayerPrefs.SetString("Ranking", novoJson);
        PlayerPrefs.Save();
    }

    // ========================================================
    // MOSTRAR RANKING NA TELA (Inalterado, mantendo seu padrão)
    // ========================================================
    public void MostrarRanking()
    {
        if (textoRanking == null) return;

        if (!PlayerPrefs.HasKey("Ranking"))
        {
            textoRanking.alignment = TextAlignmentOptions.Center;
            textoRanking.text = "RANKING";
            return;
        }

        string json = PlayerPrefs.GetString("Ranking");
        Ranking ranking = JsonUtility.FromJson<Ranking>(json);

        if (ranking == null || ranking.jogadores == null || ranking.jogadores.Count == 0)
        {
            textoRanking.alignment = TextAlignmentOptions.Center;
            textoRanking.text = "RANKING";
            return;
        }

        textoRanking.alignment = TextAlignmentOptions.TopLeft;
        string texto = "<align=center>RANKING</align>\n\n";

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
                        // 💡 PADRONIZAÇÃO: Procuramos a chave em minúsculo, garantindo que ela seja limpa
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