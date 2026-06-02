
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// 🛑 ATENÇÃO: O enum Dificuldade e a classe Pergunta FORAM REMOVIDOS daqui
// para evitar o erro de duplicidade (CS0101). Eles já são lidos globalmente.

public class QuizManagerPampa : MonoBehaviour
{
    [Header("Componentes de Tela do Quiz")]
    public TextMeshProUGUI textoPergunta;
    public TextMeshProUGUI[] textosAlternativas;

    [Header("Configuração do Pop-up de Fim de Quiz")]
    public GameObject painelResultadoFinal; 
    public TextMeshProUGUI textoResultadoFinal; 

    private Pergunta[] perguntas;
    private int perguntaAtual = 0;
    private int pontuacaoBioma = 0; // Pontuação exclusiva deste bioma (Pampa)
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

        // 📝 BANCO DE DADOS EXCLUSIVO DA Pampa
        Pergunta[] todasPerguntas = new Pergunta[]
        {
            // =========================
            // PERGUNTAS FÁCEIS 
            // =========================

            new Pergunta
            {
                enunciado = "Cerrado",
                alternativas = new string[] { "amarelo", "Pinheiro", "Macieira" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Cerrado",
                alternativas = new string[] { "amarelo", "Pinheiro", "Macieira" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Cerrado",
                alternativas = new string[] { "amarelo", "Pinheiro", "Macieira" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },


            // =========================
            // PERGUNTAS MÉDIAS 
            // =========================

            new Pergunta
            {
                enunciado = "Cerrado",
                alternativas = new string[] { "Magnésio", "Alumínio", "Prata" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Cerrado",
                alternativas = new string[] { "Magnésio", "Alumínio", "Prata" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Cerrado",
                alternativas = new string[] { "Magnésio", "Alumínio", "Prata" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Cerrado",
                alternativas = new string[] { "Magnésio", "Alumínio", "Prata" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Cerrado",
                alternativas = new string[] { "Magnésio", "Alumínio", "Prata" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },

      
      
            // =========================
            // PERGUNTAS DIFÍCEIS 
            // =========================

            new Pergunta
            {
                enunciado = "Qual",
                alternativas = new string[] { "Aumento", "Diminuição", "Resfriamento" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "Qual",
                alternativas = new string[] { "Aumento", "Diminuição", "Resfriamento" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "Qual",
                alternativas = new string[] { "Aumento", "Diminuição", "Resfriamento" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "Qual",
                alternativas = new string[] { "Aumento", "Diminuição", "Resfriamento" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

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
            Debug.Log("Acertou no Pampa!");

            if (perguntas[perguntaAtual].dificuldade == Dificuldade.Facil) pontuacaoBioma += 10;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Medio) pontuacaoBioma += 20;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Dificil) pontuacaoBioma += 30;
        }
        else
        {
            Debug.Log("Errou no Pampa!");
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

        int novaPontuacaoTotal = pontuacaoAcumuladaAnterior + pontuacaoBioma;

        PlayerPrefs.SetInt(chaveSalvamento, novaPontuacaoTotal);
        PlayerPrefs.Save();

        if (textoResultadoFinal != null)
        {
            textoResultadoFinal.text = $"<align=center><b>Parabéns!</b>\n" +
                                       $"Você concluiu o Quiz sobre o Pampa!\n\n" +
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
        Debug.Log("Saindo do Pampa e indo para o Ranking...");
        SceneManager.LoadScene("CenaRanking"); 
    }
}