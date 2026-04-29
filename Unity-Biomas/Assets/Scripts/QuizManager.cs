using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class Pergunta
{
    public string enunciado;
    public string[] alternativas;
    public int respostaCorreta;
}

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI textoPergunta;
    public TextMeshProUGUI[] textosAlternativas; // arrastar os textos dos botões aqui

    private Pergunta[] perguntas;
    private int perguntaAtual = 0;
    private int pontuacao = 0;

    void Start()
    {
        Pergunta[] todasPerguntas = new Pergunta[]
        {
        new Pergunta { enunciado = "1. Qual é a principal característica do clima na Floresta Amazônica?", alternativas = new string[] { "A) Frio e seco, com neve no inverno.", "B) Quente e muito úmido, com chuvas frequentes.", "C) Temperado, com estações do ano bem definidas.",}, respostaCorreta = 1 },
        new Pergunta { enunciado = "Pergunta 2", alternativas = new string[] { "A", "B", "C", "D" }, respostaCorreta = 1 },
        new Pergunta { enunciado = "Pergunta 3", alternativas = new string[] { "A", "B", "C", "D" }, respostaCorreta = 2 },
        new Pergunta { enunciado = "Pergunta 4", alternativas = new string[] { "A", "B", "C", "D" }, respostaCorreta = 3 },
        new Pergunta { enunciado = "Pergunta 5", alternativas = new string[] { "A", "B", "C", "D" }, respostaCorreta = 0 },
            // 👉 adiciona suas 20 perguntas aqui
        };

        // 👉 embaralha as perguntas
        Embaralhar(todasPerguntas);

        // 👉 pega só as 3 primeiras
        perguntas = new Pergunta[3];
        for (int i = 0; i < 3; i++)
        {
            perguntas[i] = todasPerguntas[i];
        }

        MostrarPergunta();
    }
    void Embaralhar(Pergunta[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, array.Length);

            Pergunta temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    void MostrarPergunta()
    {
        Pergunta p = perguntas[perguntaAtual];

        textoPergunta.text = p.enunciado;

        // Atualiza as alternativas
        for (int i = 0; i < p.alternativas.Length; i++)
        {
            textosAlternativas[i].text = p.alternativas[i];
        }
    }

    public void Responder(int indice)
    {
        if (indice == perguntas[perguntaAtual].respostaCorreta)
        {
            Debug.Log("Acertou!");
            pontuacao += 10; // 👈 soma pontos
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
            FinalizarQuiz(); // 👈 chama o final
        }
    }

    void FinalizarQuiz()
    {
        Debug.Log("Fim do quiz!");
        Debug.Log("Pontuação final: " + pontuacao);

        RankingManager.instance.SalvarPontuacao(pontuacao);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Ranking");
    }
}
