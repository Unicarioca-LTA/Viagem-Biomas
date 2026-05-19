using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

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
    public TextMeshProUGUI textoPergunta;
    public TextMeshProUGUI[] textosAlternativas;

    private Pergunta[] perguntas;

    private int perguntaAtual = 0;
    private int pontuacao = 0;
    //public GameObject PainelPergunta;
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

        Pergunta[] todasPerguntas = new Pergunta[]
        {
            // =========================
            // PERGUNTAS FÁCEIS
            // =========================

            new Pergunta
            {
                enunciado = "Qual é o maior bioma do Brasil?",
                alternativas = new string[]
                {
                    "Amazônia",
                    "Cerrado",
                    "Caatinga"
                },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "O Cerrado é conhecido como:",
                alternativas = new string[]
                {
                    "Savana brasileira",
                    "Floresta congelada",
                    "Deserto"
                },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Qual bioma possui clima muito úmido?",
                alternativas = new string[]
                {
                    "Pantanal",
                    "Amazônia",
                    "Pampa"
                },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            // =========================
            // PERGUNTAS MÉDIAS
            // =========================

            new Pergunta
            {
                enunciado = "Qual bioma brasileiro possui vegetação adaptada à seca?",
                alternativas = new string[]
                {
                    "Caatinga",
                    "Mata Atlântica",
                    "Pantanal"
                },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

            new Pergunta
            {
                enunciado = "O Pantanal é conhecido principalmente por:",
                alternativas = new string[]
                {
                    "Neve intensa",
                    "Áreas alagadas",
                    "Grandes montanhas"
                },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },

            new Pergunta
            {
                enunciado = "A Mata Atlântica foi muito afetada por:",
                alternativas = new string[]
                {
                    "Urbanização",
                    "Geleiras",
                    "Vulcões"
                },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

            // =========================
            // PERGUNTAS DIFÍCEIS
            // =========================

            new Pergunta
            {
                enunciado = "Qual bioma ocupa quase metade do território brasileiro?",
                alternativas = new string[]
                {
                    "Cerrado",
                    "Caatinga",
                    "Amazônia"
                },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "O bioma Pampa está localizado principalmente em qual região?",
                alternativas = new string[]
                {
                    "Sul",
                    "Norte",
                    "Nordeste"
                },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "Qual bioma sofre com queimadas frequentes?",
                alternativas = new string[]
                {
                    "Cerrado",
                    "Pantanal",
                    "Todos os anteriores"
                },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Dificil
            }
        };

        // =========================
        // SEPARA POR DIFICULDADE
        // =========================

        List<Pergunta> faceis = new List<Pergunta>();
        List<Pergunta> medias = new List<Pergunta>();
        List<Pergunta> dificeis = new List<Pergunta>();

        foreach (Pergunta p in todasPerguntas)
        {
            if (p.dificuldade == Dificuldade.Facil)
            {
                faceis.Add(p);
            }
            else if (p.dificuldade == Dificuldade.Medio)
            {
                medias.Add(p);
            }
            else
            {
                dificeis.Add(p);
            }
        }

        // =========================
        // EMBARALHA CADA LISTA
        // =========================

        Embaralhar(faceis);
        Embaralhar(medias);
        Embaralhar(dificeis);

        // =========================
        // PEGA:
        // 1 FÁCIL
        // 1 MÉDIA
        // 1 DIFÍCIL
        // =========================

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

            // =========================
            // PONTUAÇÃO POR DIFICULDADE
            // =========================

            if (perguntas[perguntaAtual].dificuldade == Dificuldade.Facil)
            {
                pontuacao += 10;
            }
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Medio)
            {
                pontuacao += 20;
            }
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Dificil)
            {
                pontuacao += 30;
            }
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
            FinalizarQuiz();
        }
    }

    void FinalizarQuiz()
    {
        Debug.Log("Fim do quiz!");
        Debug.Log("Pontuação final: " + pontuacao);

        // 👇 salva nome + pontuação no ranking
        RankingManager.instance.SalvarPontuacao(pontuacao);

        // 👇 vai para a cena do ranking
        
    }
}