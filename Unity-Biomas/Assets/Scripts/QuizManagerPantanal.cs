using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// 🛑 ATENÇÃO: O enum Dificuldade e a classe Pergunta FORAM REMOVIDOS daqui
// para evitar o erro de duplicidade (CS0101). Eles já são lidos globalmente.

public class QuizManagerPantanal : MonoBehaviour
{
    [Header("Componentes de Tela do Quiz")]
    public TextMeshProUGUI textoPergunta;
    public TextMeshProUGUI[] textosAlternativas;

    [Header("Configuração do Pop-up de Fim de Quiz")]
    public GameObject painelResultadoFinal; 
    public TextMeshProUGUI textoResultadoFinal; 

    private Pergunta[] perguntas;
    private int perguntaAtual = 0;
    private int pontuacaoBioma = 0; // Pontuação exclusiva deste bioma (Pantanal)
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

        // 📝 BANCO DE DADOS EXCLUSIVO DA Pantanal
        Pergunta[] todasPerguntas = new Pergunta[]
        {
            // =========================
            // PERGUNTAS FÁCEIS 
            // =========================

            new Pergunta
            {
                enunciado = "O Pantanal é mundialmente conhecido por qual característica geográfica?",
                alternativas = new string[] { "É a maior montanha do Brasil", "É a maior planície alagável (que inunda) do mundo", "É o deserto mais seco do planeta" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Qual é a ave símbolo do Pantanal, conhecida por seu bico longo e pescoço com uma faixa vermelha? ",
                alternativas = new string[] { "Tuiuiú", "Pinguim", "Beija-flor" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "O que acontece com o Pantanal durante o verão? ",
                alternativas = new string[] { "Faz muito frio e neva em toda a região", "É a época das secas e os rios desaparecem", "É o período de calor e de muitas chuvas (época da cheia)" },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "O Pantanal é considerado pela UNESCO como",
                alternativas = new string[] { "Uma cidade fantasma", "Patrimônio Natural Mundial", "O menor deserto do Brasil" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "O Tuiuiú é a ave símbolo de qual bioma brasileiro?",
                alternativas = new string[] { "Amazônia", "Caatinga", "Pantanal" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },

            // =========================
            // PERGUNTAS MÉDIAS 
            // =========================

            new Pergunta
            {
                enunciado = "Como o relevo de planície do Pantanal influencia o bioma na época das cheias? ",
                alternativas = new string[] { "A água escorre muito rápido para o oceano", "A água se acumula no terreno plano, inundando grandes áreas", "A água congela e forma grandes pistas de gelo" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "No inverno pantaneiro, os rios secam e sobra o barro. Para que esse solo costuma ser utilizado pelo homem?",
                alternativas = new string[] { "Para a construção de estádios de futebol", "Como área de pastagem para o gado", "Para a fabricação de sorvetes" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O Pantanal é o menor bioma brasileiro. Em quais estados do Brasil ele está localizado?",
                alternativas = new string[] { "Mato Grosso e Mato Grosso do Sul", "Rio de Janeiro e São Paulo", "Amazonas e Pará" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Qual destas plantas é um exemplo de vegetação que vive flutuando nas áreas inundadas do Pantanal?",
                alternativas = new string[] { "Ipê-rosa", "Aguapé", "Cacto Mandacaru" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O assoreamento dos rios é uma ameaça ao Pantanal. O que causa esse problema?",
                alternativas = new string[] { "O excesso de peixes nos rios", "O uso indevido do solo ao redor dos rios, que faz a terra 'cair' dentro da água", "A falta de luz solar durante o dia" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },

      
            new Pergunta
            {
                enunciado = "Além do Tuiuiú, qual destes animais típicos do Pantanal está infelizmente ameaçado de extinção?",
                alternativas = new string[] { "Onça-pintada", "Elefante-africano", "Urso-polar" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

      
            new Pergunta
            {
                enunciado = "O Pantanal recebe água de várias regiões ao seu redor. Qual é o nome do período em que os rios transbordam e cobrem a terra?",
                alternativas = new string[] { "Época da Nevasca", "Época da Cheia", "Época da Queimada" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },

      
            new Pergunta
            {
                enunciado = "O cervo-do-pantanal é o maior cervídeo da América do Sul. Qual é a sua principal adaptação para viver nas áreas alagadas?",
                alternativas = new string[] { "Ele possui membranas entre os dedos das patas para não afundar no barro", "Ele consegue voar sobre as áreas inundadas para fugir das onças", "Ele hiberna debaixo da água durante todo o verão chuvoso" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

      
            new Pergunta
            {
                enunciado = "Qual destes problemas ambientais é agravado pelas mudanças climáticas e ameaça o Pantanal com a destruição da mata e morte de animais?",
                alternativas = new string[] { "O excesso de plantio de árvores nativas", "Incêndios florestais intensificados por longos períodos de seca", "O congelamento das águas dos rios durante o verão" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },

      
            new Pergunta
            {
                enunciado = "O Pantanal é cercado por outros biomas. Quando usamos agrotóxicos na agricultura ao redor dele, o que acontece? ",
                alternativas = new string[] { "Os produtos químicos são levados pela água da chuva para dentro do Pantanal", "Os agrotóxicos se transformam em flores coloridas ao entrar no bioma", "Nada acontece, pois o Pantanal é protegido pela natureza" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

      
            new Pergunta
            {
                enunciado = "Qual o principal impacto dos incêndios criminosos na biodiversidade do Pantanal?",
                alternativas = new string[] { "Aumento da fertilidade natural do solo", "Morte de animais e destruição de habitats", "Diminuição do calor na região" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },

      
      
            // =========================
            // PERGUNTAS DIFÍCEIS 
            // =========================

            new Pergunta
            {
                enunciado = "O Pantanal é tecnicamente uma 'bacia de sedimentação'. Como essa característica geológica influencia o ciclo das cheias?",
                alternativas = new string[] { "Ela faz com que o terreno seja muito inclinado, acelerando a descida das águas para o mar", "Ela cria uma região de relevo muito plano e baixo, onde a água de rios vizinhos perde velocidade e se espalha pela planície", "Ela impede que o solo absorva qualquer gota de água, mantendo as lagoas salgadas o ano todo" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "A sobrevivência de muitos peixes e plantas no Pantanal depende do fenômeno da 'decoada'. O que caracteriza esse processo biológico?",
                alternativas = new string[] { "É a migração em massa de jacarés para as montanhas durante a cheia", "É a alteração na qualidade da água causada pela decomposição de matéria orgânica inundada, que consome o oxigênio e altera a cor do rio", "É o congelamento das raízes dos aguapés devido às frentes frias vindas do Sul" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "O Pantanal possui uma vegetação 'mosaico'. Por que ela recebe esse nome técnico? ",
                alternativas = new string[] { "Porque a vegetação é composta por plantas de plástico coladas umas nas outras", "Porque o bioma reúne características de vários vizinhos, como Cerrado, Amazônia e Mata Atlântica, dependendo da umidade e altitude", "Porque todas as plantas do Pantanal possuem folhas com formatos geométricos perfeitos" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "Um dos maiores perigos para os rios pantaneiros é o 'assoreamento'. Qual a relação entre a agricultura no entorno (planalto) e esse fenômeno na planície?",
                alternativas = new string[] { "O uso de agrotóxicos faz com que os rios fiquem mais profundos e largos", "A retirada da vegetação nas áreas altas faz com que a terra seja levada pela chuva até o fundo dos rios da planície, entupindo-os", "O plantio de soja atrai aves que bebem toda a água dos rios antes que cheguem ao Pantanal" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },


            new Pergunta
            {
                enunciado = "Por que o Pantanal é considerado um 'ecossistema pulsante'?",
                alternativas = new string[] { "Porque o bioma inteiro vibra devido aos trovões frequentes durante o verão", "Porque sua vida e paisagem são totalmente regadas e transformadas pelo ciclo anual de alternância entre seca e inundação", "Porque o coração dos animais pantaneiros bate mais rápido do que em qualquer outro lugar" },
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
            Debug.Log("Acertou no Pantanal!");

            if (perguntas[perguntaAtual].dificuldade == Dificuldade.Facil) pontuacaoBioma += 10;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Medio) pontuacaoBioma += 20;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Dificil) pontuacaoBioma += 30;
        }
        else
        {
            Debug.Log("Errou no Pantanal!");
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
                                       $"Você concluiu o Quiz sobre o Pantanal!\n\n" +
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
        Debug.Log("Saindo do Pantanal e indo para o Ranking...");
        SceneManager.LoadScene("CenaRanking"); 
    }
}