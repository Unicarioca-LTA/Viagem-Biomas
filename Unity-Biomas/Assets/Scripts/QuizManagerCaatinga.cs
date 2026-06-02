using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// 🛑 ATENÇÃO: O enum Dificuldade e a classe Pergunta FORAM REMOVIDOS daqui
// para evitar o erro de duplicidade (CS0101). Eles já são lidos globalmente.

public class QuizManagerCaatinga : MonoBehaviour
{
    [Header("Componentes de Tela do Quiz")]
    public TextMeshProUGUI textoPergunta;
    public TextMeshProUGUI[] textosAlternativas;

    [Header("Configuração do Pop-up de Fim de Quiz")]
    public GameObject painelResultadoFinal; 
    public TextMeshProUGUI textoResultadoFinal; 

    private Pergunta[] perguntas;
    private int perguntaAtual = 0;
    private int pontuacaoBioma = 0; // Pontuação exclusiva deste bioma (Caatinga)
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

        // 📝 BANCO DE DADOS EXCLUSIVO DA CAATINGA
        Pergunta[] todasPerguntas = new Pergunta[]
        {
            // =========================
            // PERGUNTAS FÁCEIS 
            // =========================

            new Pergunta
            {
                enunciado = "O nome Caatinga tem origem indígena. O que esse termo significa?",
                alternativas = new string[] { "Mata Verde e Úmida", "Mata Branca", "Deserto Sem Vida" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Qual é a principal característica das plantas da Caatinga para sobreviverem à seca?",
                alternativas = new string[] { "Folhas gigantes e cheias de água", "Vegetação seca, espinhosa e com poucas folhas", "Plantas que morrem totalmente no verão e renascem do zero" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Onde a Caatinga está localizada predominantemente no Brasil?",
                alternativas = new string[] { " No Sul do país, onde faz muito frio", "No Sertão Nordestino", " No litoral do Rio de Janeiro" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            new Pergunta
            {
                enunciado = "Qual destas plantas é um cacto famoso da Caatinga que serve de alimento para os animais?",
                alternativas = new string[] { " Mandacaru", " Pinheiro", "Girassol" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },


            // =========================
            // PERGUNTAS MÉDIAS 
            // =========================

            new Pergunta
            {
                enunciado = "Por que a Caatinga não pode ser considerada um deserto de verdade?",
                alternativas = new string[] { "Porque ela possui uma grande biodiversidade e é considerada um campo seco", " Porque chove todos os dias, mas a terra não segura a água", "Porque existem praias dentro da Caatinga" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Árvores como o juazeiro e a baraúna são encontradas em quais áreas da Caatinga?",
                alternativas = new string[] { "Somente nas áreas de areia da praia", " Nas áreas onde o solo tem melhores condições de umidade", " Em cima das montanhas mais altas e geladas" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O que são plantas xerófitas, termo usado para descrever a vegetação da Caatinga?",
                alternativas = new string[] { "Plantas que só vivem dentro da água dos rios", "Plantas adaptadas para viver em climas secos e com pouca água", "Plantas que não possuem raízes" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Além do desmatamento para lenha, qual ação humana tem transformado áreas da Caatinga em quase desertos?",
                alternativas = new string[] { "O plantio excessivo de árvores nativas", "O manejo inadequado do solo e o corte da vegetação", "A criação de parques de preservação ambiental" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Qual destes animais sofre com o tráfico na Caatinga?",
                alternativas = new string[] { "Asa branca", "Pinguim-de-magalhães", "Urso-pardo" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

             new Pergunta
            {
                enunciado = "Uma característica do bioma Caatinga:",
                alternativas = new string[] { "A região mais úmida do Brasil", "A região mais árida (seca) do país", "Uma região coberta por neve o ano todo" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },

              new Pergunta
            {
                enunciado = "A Caatinga é o único bioma exclusivamente brasileiro. Qual destas aves, famosa por sua cor vibrante, é um símbolo da conservação nesta região?",
                alternativas = new string[] { "Ararinha-azul", "Pinguim", "Tucano-toco" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

              new Pergunta
            {
                enunciado = "Durante a seca, a vegetação da Caatinga parece estar 'morta' ou sem cor. O que acontece com a maioria das árvores nesse período para economizar água?",
                alternativas = new string[] { "Elas perdem totalmente as folhas (caducifólia)", "Elas mudam de lugar caminhando pelo solo pedregoso", "Elas florescem e dão frutos suculentos no auge do calor" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

              new Pergunta
            {
                enunciado = "O Juazeiro é uma árvore muito especial na Caatinga. Qual é o seu grande 'superpoder' durante os meses de seca severa?",
                alternativas = new string[] { "Ele permanece sempre verde, sem perder as folhas", " Ele se transforma em um cacto cheio de espinhos", "Ele desaparece debaixo da terra e só volta quando chove" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

              new Pergunta
            {
                enunciado = "O solo da Caatinga costuma ser raso e pedregoso. Como isso afeta o bioma quando ocorrem as chuvas rápidas e fortes do Sertão?",
                alternativas = new string[] { " A água escorre rapidamente pela superfície em vez de infiltrar profundamente", "O solo vira uma esponja que guarda água por dez anos", "A água vira vapor antes mesmo de tocar o chão" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

              new Pergunta
            {
                enunciado = "Em qual região brasileira a Caatinga está predominantemente localizada?",
                alternativas = new string[] { " Região Sul", "Região Nordeste", "Região Norte" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
      
            // =========================
            // PERGUNTAS DIFÍCEIS 
            // =========================

            new Pergunta
            {
                enunciado = "A Caatinga é o único bioma 100% brasileiro. O que significa dizer que ela possui um alto índice de espécies endêmicas?",
                alternativas = new string[] { "Significa que são espécies que migram para outros países durante a seca", "Significa que são espécies que só existem naturalmente nessa região e em nenhum outro lugar do mundo", "Significa que são espécies que foram trazidas de outros continentes e se adaptaram ao sertão" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "As plantas da Caatinga utilizam estratégias morfológicas para evitar a evapotranspiração excessiva. Qual das alternativas descreve corretamente uma dessas adaptações?",
                alternativas = new string[] { "A substituição de folhas por espinhos e a presença de tecidos que armazenam água", "O desenvolvimento de folhas largas e finas para dissipar o calor rapidamente", "A manutenção de estômatos abertos durante todo o dia para resfriar a planta" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "O processo de desertificação é uma ameaça real em partes da Caatinga. Qual a principal diferença entre a Caatinga preservada e uma área desertificada?",
                alternativas = new string[] { "A Caatinga preservada é um deserto natural, enquanto a área desertificada possui florestas.", "A desertificação é causada pela ação humana (antrópica), transformando o campo seco em solo improdutivo e sem vegetação.", "A desertificação é um processo que traz mais umidade e chuvas para o bioma" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "O juazeiro é uma árvore que se destaca na Caatinga por permanecer verde mesmo durante a seca mais severa. Como isso é biologicamente possível?",
                alternativas = new string[] { " Ele possui raízes extremamente profundas que alcançam os lençóis freáticos", "Ele absorve a umidade diretamente do ar através de suas flores", "Ele entra em um estado de hibernação e para de precisar de água" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Dificil
            },

            new Pergunta
            {
                enunciado = "Por que o desvio de rios e as práticas agrícolas inadequadas são considerados ameaças críticas ao equilíbrio da Caatinga? ",
                alternativas = new string[] { "Porque impedem que os animais façam suas migrações anuais para o oceano", "Porque alteram o ciclo hidrológico natural e intensificam os efeitos da seca através de ações antrópicas.", "Porque tornam o solo fértil demais, o que mata as plantas nativas do bioma" },
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
            Debug.Log("Acertou na Caatinga!");

            if (perguntas[perguntaAtual].dificuldade == Dificuldade.Facil) pontuacaoBioma += 10;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Medio) pontuacaoBioma += 20;
            else if (perguntas[perguntaAtual].dificuldade == Dificuldade.Dificil) pontuacaoBioma += 30;
        }
        else
        {
            Debug.Log("Errou na Caatinga!");
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
                                       $"Você concluiu o Quiz sobre a Caatinga!\n\n" +
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
        Debug.Log("Saindo da Caatinga e indo para o Ranking...");
        SceneManager.LoadScene("CenaRanking"); 
    }
}