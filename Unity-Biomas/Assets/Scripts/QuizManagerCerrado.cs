using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// 🛑 ATENÇÃO: O enum Dificuldade e a classe Pergunta FORAM REMOVIDOS daqui
// para evitar o erro de duplicidade (CS0101). Eles já são lidos globalmente.

public class QuizManagerCerrado : MonoBehaviour
{
    [Header("Componentes de Tela do Quiz")]
    public TextMeshProUGUI textoPergunta;
    public TextMeshProUGUI[] textosAlternativas;

    [Header("Configuração do Pop-up de Fim de Quiz")]
    public GameObject painelResultadoFinal; 
    public TextMeshProUGUI textoResultadoFinal; 

    private Pergunta[] perguntas;
    private int perguntaAtual = 0;
    private int pontuacaoBioma = 0; // Pontuação exclusiva deste bioma (Cerrado)
    public int contador = 0;

    public void DesativarPainelPergunta(GameObject painel)
    {
        if (contador == perguntas.Length)
        {
            painel.SetActive(false);
        }
    }

    void Start()
    {
        if (painelResultadoFinal != null)
        {
            painelResultadoFinal.SetActive(false);
        }

        // 📝 BANCO DE DADOS EXCLUSIVO DO CERRADO
        Pergunta[] todasPerguntas = new Pergunta[]
        {
            // =========================
            // PERGUNTAS FÁCEIS (Cerrado)
            // =========================
            new Pergunta
            {
                enunciado = "O Cerrado é considerado qual tipo de vegetação?",
                alternativas = new string[] { "Savana", "Floresta Tropical", "Tundra" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "Qual destas frutas é típica e muito famosa no Cerrado?",
                alternativas = new string[] { "Açaí", "Pequi", "Cupuaçu" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "Qual é a principal característica das árvores do Cerrado?",
                alternativas = new string[] { "Troncos tortuosos e casca grossa", "Muito altas e retas", "Folhas gigantes e perenes" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },

            // =========================
            // PERGUNTAS MÉDIAS (Cerrado)
            // =========================
            new Pergunta
            {
                enunciado = "Qual animal símbolo do Cerrado corre risco de extinção e tem pernas longas?",
                alternativas = new string[] { "Mico-leão-dourado", "Lobo-guará", "Onça-pintada" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O Cerrado é considerado o 'Berço das Águas' do Brasil por quê?",
                alternativas = new string[] { "Chove o ano inteiro sem parar", "Abriga nascentes de grandes bacias hidrográficas", "É coberto por grandes lagos" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Qual o tipo de clima predominante no Cerrado?",
                alternativas = new string[] { "Equatorial úmido", "Tropical sazonal (chuvoso e seco)", "Semiárido" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },

            // =========================
            // PERGUNTAS DIFÍCEIS (Cerrado)
            // =========================
            new Pergunta
            {
                enunciado = "Em termos de extensão territorial no Brasil, o Cerrado ocupa qual posição?",
                alternativas = new string[] { "1º maior", "2º maior", "3º maior" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "Qual o principal fator abiótico natural que moldou a evolução da flora do Cerrado?",
                alternativas = new string[] { "O fogo/queimadas naturais", "O congelamento do solo", "Inundações constantes" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "Como é classificado o solo predominante do Cerrado?",
                alternativas = new string[] { "Rico em matéria orgânica", "Ácido e rico em ferro e alumínio", "Calcário e muito fértil" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            }
        };

        List<Pergunta> faceis = new List<Pergunta>();
        List<Pergunta> medias = new List<Pergunta>();
        List<Pergunta> dificeis = new List<Pergunta>();

        foreach (Pergunta p in todasPerguntas)
        {
            if (p.dificuldade == Dificuldade.Facil) faceis.Add(p);
            else if (p.dificuldade == Dificuldade.Medio) medias.Add(p);
            else dificeis.Add(p);
        }

        Embaralhar(faceis);
        Embaralhar(medias);
        Embaralhar(dificeis);

        // Monta a partida com 1 Fácil, 1 Média e 1 Difícil aleatórias
        perguntas = new Pergunta[]
        {
            faceis[0],
            medias[0],
            dificeis[0]
        };

        MostrarPergunta();
    }

    void Embaralhar(List<Pergunta> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int randomIndex = Random.Range(i, lista.Count);
            Pergunta temp = lista[i];
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
    }

    void MostrarPergunta()
    {
        Pergunta p = perguntas[perguntaAtual];
        textoPergunta.text = p.enunciado;

        for (int i = 0; i < p.alternativas.Length; i++)
        {
            textosAlternativas[i].text = p.alternativas[i];
        }
    }

    public void Responder(int indice)
    {
        contador++;

        if (indice == perguntas[perguntaAtual].respostaCorreta)
        {
            Debug.Log("Acertou no Cerrado!");

            if (perguntas[perguntaAtual].dificuldade == Dificuldade.Facil) pontuacaoBioma += 10;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Medio) pontuacaoBioma += 20;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Dificil) pontuacaoBioma += 30;
        }
        else
        {
            Debug.Log("Errou no Cerrado!");
        }

        perguntaAtual++;

        if (perguntaAtual < perguntas.Length)
        {
            MostrarPergunta();
        }
        else
        {
            ExibirPainelParabens(); 
        }
    }

    void ExibirPainelParabens()
    {
        string nomeJogadorAtual = "Jogador";
        if (GameHandler.instance != null && !string.IsNullOrEmpty(GameHandler.instance.nomeJogador))
        {
            nomeJogadorAtual = GameHandler.instance.nomeJogador.Trim();
        }

        string chaveSalvamento = "Pontos_" + nomeJogadorAtual.ToLower();

        int pontuacaoAcumuladaAnterior = 0;
        if (PlayerPrefs.HasKey(chaveSalvamento))
        {
            pontuacaoAcumuladaAnterior = PlayerPrefs.GetInt(chaveSalvamento);
        }

        // Soma os pontos que ele já tinha da Amazônia (ou de outras jogadas) com os novos do Cerrado
        int novaPontuacaoTotal = pontuacaoAcumuladaAnterior + pontuacaoBioma;

        PlayerPrefs.SetInt(chaveSalvamento, novaPontuacaoTotal);
        PlayerPrefs.Save();

        if (textoResultadoFinal != null)
        {
            // Ajustado o texto dinâmico para dizer "Cerrado"
            textoResultadoFinal.text = $"<align=center><b>Parabéns!</b>\n" +
                                       $"Você concluiu o Quiz sobre o Cerrado!\n\n" +
                                       $"Sua pontuação no Bioma: <color=green>{pontuacaoBioma} pts</color>\n" +
                                       $"Sua Pontuação Total Acumulada: <color=yellow>{novaPontuacaoTotal} pts</color></b></align>";
        }

        if (painelResultadoFinal != null)
        {
            painelResultadoFinal.SetActive(true);
        }
    }

    public void BotaoFinalizarCena()
    {
        Debug.Log("Saindo do Cerrado e indo para o Ranking...");
        SceneManager.LoadScene("CenaRanking"); 
    }
}