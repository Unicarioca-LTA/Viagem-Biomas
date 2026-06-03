using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// 🛑 ATENÇÃO: O enum Dificuldade e a classe Pergunta FORAM REMOVIDOS daqui
// para evitar o erro de duplicidade (CS0101). Eles já são lidos globalmente.

public class QuizManagerMataAtlantica : MonoBehaviour
{
    [Header("Componentes de Tela do Quiz")]
    public TextMeshProUGUI textoPergunta;
    public TextMeshProUGUI[] textosAlternativas;

    [Header("Configuração do Pop-up de Fim de Quiz")]
    public GameObject painelResultadoFinal; 
    public TextMeshProUGUI textoResultadoFinal; 

    private Pergunta[] perguntas;
    private int perguntaAtual = 0;
    private int pontuacaoBioma = 0; // Pontuação exclusiva deste bioma (MataAtlantica)
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

        // 📝 BANCO DE DADOS EXCLUSIVO DA MataAtlantica
        Pergunta[] todasPerguntas = new Pergunta[]
        {
            // =========================
            // PERGUNTAS FÁCEIS 
            // =========================

            new Pergunta
            {
                enunciado = "Onde se localiza a maior parte da Mata Atlântica no Brasil?",
                alternativas = new string[] { "No centro do país, longe do mar", "Ao longo da faixa litorânea (costa), de norte a sul", "Apenas na região da Floresta Amazônica" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Qual é a principal característica das árvores da Mata Atlântica?",
                alternativas = new string[] { "São árvores baixas, retorcidas e espalhadas", "É uma floresta densa, fechada e com árvores de grande e médio porte", "São apenas arbustos pequenos que perdem as folhas no inverno" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Qual destes animais é um símbolo exclusivo da Mata Atlântica e corre risco de extinção? ",
                alternativas = new string[] { "Mico-leão-dourado", "Camelo", "Canguru" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Aproximadamente quanta população brasileira vive na área original da Mata Atlântica?",
                alternativas = new string[] { "Cerca de 10% da população", "Cerca de 70% da população", "Quase ninguém vive nessa área" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },


            // =========================
            // PERGUNTAS MÉDIAS 
            // =========================

            new Pergunta
            {
                enunciado = "Por que chove tanto na Mata Atlântica?",
                alternativas = new string[] { "Porque as montanhas barram o vapor d'água do oceano, fazendo-o esfriar e virar chuva", "Porque o sol nunca aparece nesse bioma", "Porque a floresta é plantada dentro de lagos gigantes" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Atualmente, quanto resta da cobertura original da Mata Atlântica? ",
                alternativas = new string[] { "Quase toda a floresta (90%)", "Apenas cerca de 12,4%", "A floresta já desapareceu totalmente (0%)" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O Pau-brasil é uma árvore histórica do bioma Mata Atlântica. Qual é a sua situação atual? ",
                alternativas = new string[] { "É a árvore mais comum e abundante nas cidades", "Está na lista de espécies ameaçadas de extinção", "É uma espécie que não existe mais em lugar nenhum" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Além dos animais e plantas, quem são os 'povos tradicionais' que habitam e protegem o bioma Mata Atlântica? ",
                alternativas = new string[] { "Apenas turistas estrangeiros", "Indígenas, quilombolas, caiçaras e ribeirinhos", "Pessoas que vivem apenas em prédios de luxo" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

            new Pergunta
            {
                enunciado = "Qual o principal motivo da 'fragmentação' do habitat dos animais na Mata Atlântica?",
                alternativas = new string[] { "O nascimento natural de novas montanhas", "A urbanização desordenada e a expansão da agropecuária", "O excesso de chuva que quebra as árvores" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Se compararmos a Mata Atlântica com a Amazônia, o que elas têm em comum?",
                alternativas = new string[] { "Ambas são florestas tropicais com altíssima biodiversidade", "Ambas ficam localizadas no interior seco do Nordeste", "Ambas possuem clima desértico" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "A Mata Atlântica é chamada de 'floresta tropical pluvial'. O que a palavra 'pluvial' indica sobre esse ambiente? ",
                alternativas = new string[] { "Que é um lugar onde venta muito forte o dia todo", "Que é uma região com altos índices de chuva", "PrQue é um bioma onde o solo é coberto por areia de praiaata" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O mico-leão-dourado e a jaguatirica são animais que vivem no bioma Mata Atlântica. O que acontece com eles quando a mata é dividida em 'fragmentos'?",
                alternativas = new string[] { "Eles ficam isolados em pequenos pedaços de floresta, o que dificulta sua sobrevivência", "Eles aprendem a viver facilmente dentro de apartamentos e casas", "Eles mudam de cor para se esconder nas estradas de asfalto" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

            new Pergunta
            {
                enunciado = "Qual é a temperatura média anual da Mata Atlântica, que permite a existência de tantas plantas diferentes?",
                alternativas = new string[] { "Muito gelada, sempre abaixo de 0°C", "Extremamente quente, chegando a 50°C todos os dias", "Uma temperatura agradável, próxima dos 21°C" },
                respostaCorreta = 2,
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
                enunciado = "Dentre as plantas abaixo, quais são exemplos típicos que encontramos na Mata Atlântica? ",
                alternativas = new string[] { "Bromélias, Ipês e Jequitibá-rosa", "Apenas cactos e plantas com espinhos", "Árvores de maçã e de pera em toda a floresta" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

      
            // =========================
            // PERGUNTAS DIFÍCEIS 
            // =========================

            new Pergunta
            {
                enunciado = "A Mata Atlântica é classificada como uma 'Hotspot' de biodiversidade mundial. O que esse termo técnico indica sobre o bioma?",
                alternativas = new string[] { "Que é uma região com temperaturas tão elevadas que impedem a vida humana", "Que é uma área com altíssima riqueza de espécies únicas (endêmicas), mas que está extremamente ameaçada, restando pouco de sua área original. ", "Que é um bioma que se recupera sozinho e rapidamente, sem necessidade de projetos de conservação." },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "O fenômeno das 'Chuvas Orográficas' (ou de relevo) é fundamental para a Mata Atlântica. Como as montanhas da costa influenciam esse processo?",
                alternativas = new string[] { "As montanhas funcionam como uma barreira física que força as massas de ar úmidas a subir, resfriar e condensar, gerando chuva", "As montanhas impedem que o oxigênio saia da floresta, mantendo o ar parado", "As montanhas absorvem toda a água do oceano através de suas rochas, secando o litoral" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "A 'Fragmentação de Habitat' é um dos maiores problemas deste bioma. Qual é a principal consequência biológica de uma floresta dividida em pequenos 'pedaços' isolados?",
                alternativas = new string[] { "O aumento da diversidade genética, pois cada pedaço de mata cria animais diferentes", "O chamado 'efeito de borda' e o isolamento de populações, que dificulta a reprodução e aumenta o risco de extinção local", "A transformação automática das árvores de grande porte em arbustos pequenos para caber no espaço" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "A Mata Atlântica é uma 'Floresta Tropical Pluvial do tipo Costeira'. Qual fator é o principal responsável por manter sua exuberância mesmo em áreas de solo raso em montanhas? ",
                alternativas = new string[] { "A adubação constante realizada pelos 140 milhões de habitantes da região", "A alta umidade constante e a ciclagem de nutrientes da própria floresta, similar à Amazônia", "O fato de as raízes das árvores se alimentarem diretamente do sal do Oceano Atlântico" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "Povos tradicionais como Caiçaras e Quilombolas dependem da 'subsistência' na Mata Atlântica. No contexto ecológico, o que isso significa para a conservação do bioma?",
                alternativas = new string[] {"Que essas comunidades retiram recursos de forma predatória até que a natureza se esgote", "Que eles utilizam os recursos naturais de forma sustentável, mantendo o equilíbrio do ecossistema para as futuras gerações", "Que eles vivem exclusivamente da tecnologia urbana e não utilizam nada da floresta" },
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
            Debug.Log("Acertou na Mata Atlântica!");

            if (perguntas[perguntaAtual].dificuldade == Dificuldade.Facil) pontuacaoBioma += 10;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Medio) pontuacaoBioma += 20;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Dificil) pontuacaoBioma += 30;
        }
        else
        {
            Debug.Log("Errou na Mata Atlântica!");
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
                                       $"Você concluiu o Quiz sobre o Mata Atlântica!\n\n" +
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
        Debug.Log("Saindo da Mata Atlântica e indo para o Ranking...");
        SceneManager.LoadScene("CenaRanking"); 
    }
}