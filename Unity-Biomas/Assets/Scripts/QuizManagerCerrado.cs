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
                enunciado = "O Cerrado é conhecido como o segundo maior bioma do Brasil. Qual é o seu tipo de vegetação predominante?",
                alternativas = new string[] { "Árvores gigantes e floresta muito fechada", "Árvores baixas de troncos retorcidos e arbustos", "Vegetação composta apenas por grama e sem árvores" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "Como é o clima no Cerrado durante a maior parte do ano?",
                alternativas = new string[] { "Sempre chuvoso e frio em todas as estações", "Clima tropical, com um período de chuvas e outro de seca", "Desértico, sem nenhuma chuva durante o ano" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "Qual destes animais típicos do Cerrado corre risco de extinção devido à destruição do seu habitat?",
                alternativas = new string[] { "Elefante", "Tamanduá-bandeira", "Leão" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "O Cerrado é famoso por suas flores coloridas. Qual destas árvores é um símbolo do bioma?",
                alternativas = new string[] { "Ipê-amarelo", "Pinheiro-do-paraná", "Macieira" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },


            // =========================
            // PERGUNTAS MÉDIAS (Cerrado)
            // =========================
            new Pergunta
            {
                enunciado = "Por que muitas plantas do Cerrado possuem cascas grossas e raízes muito profundas?",
                alternativas = new string[] { "Para se protegerem do frio intenso e da neve", "Para sobreviverem aos períodos de seca e incêndios naturais", "Para evitarem que os animais comam seus frutos" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O solo do Cerrado é naturalmente ácido e pobre em nutrientes. O que ele precisa para ser usado na agricultura?",
                alternativas = new string[] { "Apenas de água em abundância", "De adubação e tratamento para corrigir a acidez (calagem)", "Não precisa de nada, pois o solo é perfeito para o plantio" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O Cerrado é chamado de 'caixa d'água do Brasil'. Qual o principal motivo desse apelido?",
                alternativas = new string[] { "Porque é o bioma onde mais chove no mundo todo", "Porque abriga nascentes de importantes rios brasileiros", "Porque possui as maiores cachoeiras do planeta" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Qual atividade humana é considerada a maior ameaça atual para a preservação do Cerrado",
                alternativas = new string[] { "O desmatamento para a expansão da agropecuária", "A construção de cidades de gelo", "A plantação de florestas de seringueiras" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Sobre as águas do Cerrado, qual problema ambiental grave tem ocorrido devido à agricultura intensa?",
                alternativas = new string[] { "O congelamento total dos rios", "A contaminação por agrotóxicos", "O aumento excessivo de peixes nos rios" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Além do tamanduá-bandeira, qual destes animais também sofre com a perda de biodiversidade no Cerrado?",
                alternativas = new string[] { "Tatu-bola", "Urso-polar", "Canguru" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O Cerrado é conhecido como a 'savana mais rica do mundo'. Qual planta é muito comum no bioma e produz um fruto amarelado muito usado na culinária regional?",
                alternativas = new string[] { "Açaí", "Cacau", "Pequi" },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Devido à sua localização estratégica no centro do Brasil, o Cerrado abriga as nascentes de grandes bacias hidrográficas. Qual é o apelido que o bioma recebe por causa disso?",
                alternativas = new string[] { "Ilha de Calor", "Pantanal Seco", "Berço das Águas" },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Um dos animais mais elegantes do Cerrado possui pernas longas e pelagem avermelhada. De qual animal estamos falando?",
                alternativas = new string[] { "Cachorro-do-mato", "Onça-parda", "Lobo-guará" },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O solo do Cerrado é rico em alguns minerais que podem dificultar o crescimento de certas plantas. Quais são os dois elementos principais encontrados em abundância nesse solo",
                alternativas = new string[] { "Sal e Magnésio", "Alumínio e Ferro", "Ouro e Prata" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },

      
      
            // =========================
            // PERGUNTAS DIFÍCEIS (Cerrado)
            // =========================
            new Pergunta
            {
                enunciado = "O Cerrado é frequentemente chamado de 'Floresta de Cabeça para Baixo'. Qual característica biológica justifica esse nome?",
                alternativas = new string[] { "As árvores crescem com os galhos voltados para o solo para evitar o sol", "O bioma possui raízes extremamente profundas e extensas, que superam o tamanho da parte aérea", "As plantas realizam fotossíntese apenas durante a noite para economizar energia" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "Os solos do Cerrado são predominantemente latossolos antigos. Qual é o impacto químico natural dessa característica na vegetação?",
                alternativas = new string[] { "Alta concentração de alumínio e ferro, o que torna o solo ácido e de baixa fertilidade", "Excesso de nutrientes orgânicos que dispensa qualquer tipo de adubação", "Solo rico em calcário natural, que mantém o pH sempre neutro" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "O Cerrado desempenha um papel hidrográfico crucial para o continente sul-americano. Por que sua preservação afeta a geração de energia em outros estados?",
                alternativas = new string[] { "Porque o bioma funciona como um reservatório que alimenta as nascentes de grandes bacias hidrográficas", "Porque o Cerrado é o único bioma brasileiro onde os rios nunca sofrem com a seca", "Porque a vegetação do Cerrado atrai nuvens de chuva diretamente para as turbinas das hidrelétricas" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "Muitas plantas do Cerrado são 'pirófitas'. O que isso significa no contexto da dinâmica deste bioma",
                alternativas = new string[] { "São plantas que morrem imediatamente ao primeiro sinal de fumaça", "São espécies que possuem mecanismos de resistência ou que até dependem do fogo natural para a quebra de dormência de sementes", "São plantas aquáticas que surgem apenas nos períodos de inundação das chapadas" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "Qual é a principal consequência ambiental da substituição da vegetação nativa por monoculturas de ciclo curto (como a soja) em relação ao ciclo da água",
                alternativas = new string[] { "Aumento da infiltração da água, tornando o solo muito encharcado o ano todo", "Diminuição da recarga dos aquíferos, pois as plantas de ciclo curto não possuem as raízes profundas que levam a água para o subsolo", "Resfriamento imediato das temperaturas locais devido à cor verde das plantações" },
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