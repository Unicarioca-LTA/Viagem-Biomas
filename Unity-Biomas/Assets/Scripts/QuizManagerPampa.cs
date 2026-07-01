
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
                enunciado = "Em qual estado brasileiro o bioma Pampa está localizado quase que totalmente? ",
                alternativas = new string[] { "Mato Grosso", "Rio Grande do Sul", "Bahia" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "O Pampa é conhecido como um campo 'limpo'. Qual é o tipo de vegetação que predomina nessa paisagem? ",
                alternativas = new string[] { "Florestas muito altas e fechadas", "Grandes áreas de gramíneas (capim) e poucos arbustos", "Desertos de areia sem nenhuma planta" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "O clima no Pampa é o Subtropical. O que isso significa para as estações do ano?",
                alternativas = new string[] { "Que não existem estações e faz calor o ano todo", "Que as 4 estações do ano são bem definidas (verão quente e inverno frio", "Que chove apenas uma vez a cada dez anos" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Qual destas aves é um símbolo muito conhecido que habita os campos do Pampa?",
                alternativas = new string[] { "Quero-quero", "Pinguim-imperial", "Arara-azul" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },


            // =========================
            // PERGUNTAS MÉDIAS 
            // =========================

            new Pergunta
            {
                enunciado = "O relevo do Pampa é marcado por planícies. Para qual atividade econômica esse terreno é muito utilizado?",
                alternativas = new string[] { "Para a construção de grandes prédios de 10 andares", "Para a criação de grandes rebanhos de bois e ovelhas (pecuária)", "Para a mineração de ouro em montanhas altas" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O solo do Pampa era originalmente fértil, mas hoje sofre com um problema chamado 'arenização'. O que causa isso?",
                alternativas = new string[] { "O excesso de plantio de árvores nativas", "A má utilização do solo pela agricultura e pecuária intensas", "O vento que traz areia do deserto do Saara" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Quanto da vegetação original do Pampa ainda está preservada, atualmente",
                alternativas = new string[] { "Quase 100%, está totalmente preservado", "Apenas cerca de 35%", "Já não resta mais nada da vegetação original" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O plantio de espécies 'exóticas' ajuda a descaracterizar o bioma. Qual árvore é um exemplo de espécie invasora no Pampa?",
                alternativas = new string[] { "Eucalipto", "Louro-pardo", "Bracatinga" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Além da criação de boi e ovelha, quais são as principais produções agrícolas do Pampa?",
                alternativas = new string[] { "Apenas frutas tropicais como manga e jaca", "Soja, arroz, milho, trigo e uva", "Café e cacau em larga escala" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            
            new Pergunta
            {
                enunciado = "Por que o Pampa é considerado um bioma 'ainda em formação'?",
                alternativas = new string[] { "Porque ele surgiu há apenas 10 anos", "Devido às suas características geológicas e botânicas em evolução", "Porque ele está sendo construído por engenheiros" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            
            new Pergunta
            {
                enunciado = "O Pampa possui uma flora diversa com cerca de 3.000 espécies. Qual destas árvores é um exemplo de planta nativa encontrada neste bioma?",
                alternativas = new string[] { "Cacto Mandacaru", "Louro-pardo", "Seringueira" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Além do quero-quero, qual destes animais é um exemplo da fauna típica que constrói ninhos no Pampa?",
                alternativas = new string[] { "Mico-leão-dourado", "Arara-canindé", "João-de-barro" },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "A introdução de espécies vindas de fora pode desequilibrar o ecossistema. Como são chamadas essas espécies, como o eucalipto, no contexto do Pampa?",
                alternativas = new string[] { "Espécies nativas", "Espécies endêmicas", "Vulcanismo ativo" },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "A atividade humana acelerada provoca a degradação do solo no Pampa. Qual o principal risco ambiental para o solo desse bioma?",
                alternativas = new string[] { "Arenização", "Inundação salina", "Espécies exóticas" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

      
      
            // =========================
            // PERGUNTAS DIFÍCEIS 
            // =========================

            new Pergunta
            {
                enunciado = "O Pampa é o único bioma brasileiro com clima Subtropical. Qual a principal consequência dessa característica para a dinâmica da vegetação ao longo do ano?",
                alternativas = new string[] { "A vegetação mantém o mesmo ritmo de crescimento constante durante os 12 meses", "Ocorre uma sazonalidade térmica marcada, onde o frio intenso do inverno pode reduzir a atividade biológica de algumas espécies", "As plantas desenvolvem espinhos para se protegerem da neve que cai diariamente no verão" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "O fenômeno da 'arenização' é um dos maiores desafios do Pampa. Como ele se diferencia quimicamente da desertificação comum?",
                alternativas = new string[] { "A arenização ocorre em solos originalmente arenosos e úmidos que perdem a cobertura vegetal, expondo depósitos de areia através da erosão", "A arenização é o processo de transformar rochas vulcânicas em florestas tropicais", "A arenização só acontece quando o sal do mar invade as plantações de soja" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "O plantio de espécies exóticas, como o Eucalipto, é uma preocupação ecológica. Qual o principal impacto dessa prática na biodiversidade nativa do Pampa?",
                alternativas = new string[] { "O eucalipto atrai animais nativos que antes não conseguiam viver no bioma", "A descaracterização do ecossistema de campo e a competição por recursos hídricos e nutrientes com as espécies originais", "O aumento da fertilidade natural do solo para as gramíneas nativas" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "O Pampa é considerado um bioma de 'formação geológica recente'. Por que isso influencia a sua classificação como um campo em evolução? ",
                alternativas = new string[] { "Porque ele foi criado por vulcões que ainda estão ativos no Rio Grande do Sul", "Porque suas características botânicas e de solo ainda refletem processos de transição e adaptação climática pós-eras glaciais", "Porque a areia do solo ainda não teve tempo de se transformar em terra fértil" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "Por que a introdução de animais domésticos (bois e ovelhas) tem um lado positivo e um negativo para o Pampa?",
                alternativas = new string[] { "Positivo porque eles comem apenas as espécies invasoras e negativo porque eles bebem toda a água dos rios", "Positivo porque a pecuária bem manejada mantém a estrutura de campo, mas negativo quando o sobrepastoreio causa a degradação e arenização do solo", "Positivo porque os animais aquecem o solo no inverno e negativo porque eles atraem pinguins para a região" },
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