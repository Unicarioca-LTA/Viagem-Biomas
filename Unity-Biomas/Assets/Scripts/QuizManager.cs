using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum Dificuldade
{
    Facil,
    Medio,
    Dificil
}

[System.Serializable]
public class Pergunta
{
    public string enunciado;
    public string[] alternativas;
    public int respostaCorreta;
    public Dificuldade dificuldade;
}

public class QuizManager : MonoBehaviour
{
    [Header("Componentes de Tela do Quiz")]
    public TextMeshProUGUI textoPergunta;
    public TextMeshProUGUI[] textosAlternativas;

    [Header("Configuração do Pop-up de Fim de Quiz")]
    public GameObject painelResultadoFinal; 
    public TextMeshProUGUI textoResultadoFinal; 

    private Pergunta[] perguntas;
    private int perguntaAtual = 0;
    private int pontuacaoBioma = 0; // Pontuação exclusiva deste bioma (Amazônia)
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

        Pergunta[] todasPerguntas = new Pergunta[]
        {
            // =========================
            // PERGUNTAS FÁCEIS
            // =========================
            new Pergunta
            {
                enunciado = "Qual é o maior bioma do Brasil?",
                alternativas = new string[] { "Amazônia", "Cerrado", "Caatinga" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "O Cerrado é conhecido como:",
                alternativas = new string[] { "Savana brasileira", "Floresta congelada", "Deserto" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "Qual bioma possui clima muito úmido?",
                alternativas = new string[] { "Pantanal", "Amazônia", "Pampa" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            // =========================
            // PERGUNTAS MÉDIAS
            // =========================
            new Pergunta
            {
                enunciado = "Qual bioma brasileiro possui vegetação adaptada à seca?",
                alternativas = new string[] { "Caatinga", "Mata Atlântica", "Pantanal" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O Pantanal é conhecido principalmente por:",
                alternativas = new string[] { "Neve intensa", "Áreas alagadas", "Grandes montanhas" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "A Mata Atlântica foi muito afetada por:",
                alternativas = new string[] { "Urbanização", "Geleiras", "Vulcões" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

            // =========================
            // PERGUNTAS DIFÍCEIS
            // =========================
            new Pergunta
            {
                enunciado = "Qual bioma ocupa quase metade do território brasileiro?",
                alternativas = new string[] { "Cerrado", "Caatinga", "Amazônia" },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "O bioma Pampa está localizado principalmente em qual região?",
                alternativas = new string[] { "Sul", "Norte", "Nordeste" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "Qual bioma sofre com queimadas frequentes?",
                alternativas = new string[] { "Cerrado", "Pantanal", "Todos os anteriores" },
                respostaCorreta = 2,
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
            Debug.Log("Acertou!");

            if (perguntas[perguntaAtual].dificuldade == Dificuldade.Facil) pontuacaoBioma += 10;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Medio) pontuacaoBioma += 20;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Dificil) pontuacaoBioma += 30;
        }
        else
        {
            Debug.Log("Errou!");
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

// ========================================================
    // ATIVA O POP-UP DE RESULTADOS (Salva no escopo do jogador atual)
    // ========================================================
void ExibirPainelParabens()
    {
        string nomeJogadorAtual = "Jogador";
        if (GameHandler.instance != null && !string.IsNullOrEmpty(GameHandler.instance.nomeJogador))
        {
            nomeJogadorAtual = GameHandler.instance.nomeJogador.Trim();
        }

        // 💡 PADRONIZAÇÃO: Chave em minúsculo aqui também!
        string chaveSalvamento = "Pontos_" + nomeJogadorAtual.ToLower();

        int pontuacaoAcumuladaAnterior = 0;
        if (PlayerPrefs.HasKey(chaveSalvamento))
        {
            pontuacaoAcumuladaAnterior = PlayerPrefs.GetInt(chaveSalvamento);
        }

        int novaPontuacaoTotal = pontuacaoAcumuladaAnterior + pontuacaoBioma;

        PlayerPrefs.SetInt(chaveSalvamento, novaPontuacaoTotal);
        PlayerPrefs.Save();

        // 6. Atualiza a interface do Pop-up
if (textoResultadoFinal != null)
{
    // Usamos as tags <align=center> e <b> para garantir a formatação por código
    // O tamanho da fonte pode ser controlado diretamente no componente, mas forçamos aqui para garantir
    textoResultadoFinal.text = $"<align=center><b>Parabéns!</b>\n" +
                               $"Você concluiu o Quiz sobre a Amazônia!\n\n" +
                               $"Sua pontuação no Bioma: <color=green>{pontuacaoBioma} pts</color>\n" +
                               $"Sua Pontuação Total: <color=yellow>{novaPontuacaoTotal} pts</color></b></align>";
}

if (painelResultadoFinal != null)
{
    painelResultadoFinal.SetActive(true);
}
    }

    // ========================================================
    // BOTÃO FINALIZAR (Chama a cena do Ranking para mostrar o placar)
    // ========================================================
    public void BotaoFinalizarCena()
    {
        Debug.Log("Indo para a cena de Ranking...");
        SceneManager.LoadScene("CenaRanking"); // Certifique-se de usar o nome correto da sua cena aqui
    }
}