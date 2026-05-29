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
                enunciado = "Qual é a principal característica do clima na Floresta Amazônica?",
                alternativas = new string[] { "Frio e seco, com neve no inverno", "Quente e muito úmido, com chuvas frequentes", "Temperado, com estações do ano bem definidas" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "A Amazônia é considerada a maior floresta tropical do mundo. Quanto do território brasileiro ela ocupa aproximadamente?",
                alternativas = new string[] { "Quase metade (49%)", "Uma pequena parte (10%)", "Apenas a região litorânea (5%)" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "Qual destas frutas é nativa da Amazônia e muito usada em sucos e tigelas?",
                alternativas = new string[] { "Jabuticaba", "Açaí", "Pera" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },
            new Pergunta
            {
                enunciado = "Qual destes animais é um símbolo da fauna amazônica?",
                alternativas = new string[] { "Pinguim", "Arara-canindé", "Camelo" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Facil
            },

            // =========================
            // PERGUNTAS MÉDIAS
            // =========================
            new Pergunta
            {
                enunciado = "Sobre o solo da Amazônia, é correto afirmar que: ",
                alternativas = new string[] { "É extremamente rico em nutrientes e minerais por natureza", "É pobre em nutrientes, pois a maior parte da riqueza está nas próprias plantas", "É um solo desértico onde poucas plantas conseguem crescer" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Por que a Amazônia é importante para o clima de outras regiões do Brasil?",
                alternativas = new string[] { "Porque ela impede que o vento chegue ao Sul", "Porque ela ajuda a regular as chuvas em quase todo o país", "Porque ela esfria o oceano Atlântico" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Se você olhar um mapa do Brasil, em qual região a maior parte do bioma Amazônia está localizada? ",
                alternativas = new string[] { "Região Sul", "Região Nordeste", "Região Norte" },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Qual é uma das maiores ameaças atuais à preservação da Floresta Amazônica? ",
                alternativas = new string[] { "O excesso de plantio de árvores nativas", "O desmatamento para expansão de garimpo ilegal e pecuária", "O turismo ecológico controlado" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Árvores como o mogno e a castanheira típicas da Amazônia são classificadas como:",
                alternativas = new string[] { "Árvores de pequeno porte (arbustos)", "Árvores de grande porte", "Plantas rasteiras que não possuem tronco" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Qual é a porcentagem aproximada de vegetação original que a Amazônia ainda possui? ",
                alternativas = new string[] { "Cerca de 80%", "Menos de 10%", "Já foi totalmente destruída" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "A Floresta Amazônica possui árvores gigantescas. Uma delas é a Sumaúma. Por que ela é conhecida como a 'escada do céu'?",
                alternativas = new string[] { "Porque suas raízes são tão grandes que formam degraus na floresta", "Porque ela é uma das árvores mais altas da floresta, podendo chegar a 60 metros de altura", "Porque seus galhos brilham durante a noite como estrelas" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "No bioma Amazônia, encontramos a Vitória-régia. O que ela é exatamente?",
                alternativas = new string[] { "Uma ave muito colorida que vive nas copas das árvores", "Uma planta aquática com folhas circulares gigantes que flutuam nos rios", "Uma espécie de peixe que salta fora da água para comer frutos" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "O solo da Amazônia é pobre em nutrientes, mas a floresta é riquíssima. Como a floresta se alimenta?",
                alternativas = new string[] { "Através da 'serrapilheira' que é a camada de folhas e galhos mortos que apodrecem e viram adubo rápido", "Através da água da chuva que já vem misturada com vitaminas do oceano", "Através do pó que vem dos vulcões vizinhos todos os dias" },
                respostaCorreta = 0,
                dificuldade = Dificuldade.Medio
            },

            new Pergunta
            {
                enunciado = "Além da extração de madeira, qual outra atividade ilegal causa grandes danos aos rios e à floresta?",
                alternativas = new string[] { "A plantação de jardins de flores exóticas", "O garimpo ilegal, que polui as águas e destrói a mata", "A construção de pistas de patinação no gelo" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "Qual é a principal função da 'serrapilheira' (camada de folhas secas no chão) para a Amazônia?",
                alternativas = new string[] { "Proteger o solo contra o frio extremo", "Reciclar nutrientes para alimentar a floresta", "Impedir que a chuva chegue às raízes" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },
            new Pergunta
            {
                enunciado = "A bacia amazônica é famosa por qual recorde mundial?",
                alternativas = new string[] { "Maior reserva de gelo do planeta", "Maior rede de rios de água doce do mundo", "Única região que não possui insetos" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Medio
            },


            // =========================
            // PERGUNTAS DIFÍCEIS
            // =========================
            new Pergunta
            {
                enunciado = "A exuberância da vegetação amazônica contrasta com a baixa fertilidade natural de grande parte de seus solos. Qual processo ecológico é o principal responsável por manter a produtividade biológica do bioma?",
                alternativas = new string[] { "A fixação de nitrogênio atmosférico pelas árvores de grande porte como o mogno", "O acúmulo milenar de minerais pesados no horizonte profundo do solo", "A ciclagem rápida de nutrientes através da decomposição da serrapilheira" },
                respostaCorreta = 2,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "A Amazônia influencia o regime de chuvas em regiões distantes, como o Sudeste do Brasil. Qual fenômeno explica essa conexão climática?",
                alternativas = new string[] { "O bloqueio de ventos secos vindos do Oceano Pacífico pela Cordilheira dos Andes", "O transporte de umidade pela evapotranspiração, conhecido como 'rios voadores'", "A criação de zonas de alta pressão que empurram as nuvens para o sul" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "O equilíbrio térmico da Amazônia é vital para o mundo. Como a floresta contribui diretamente para a mitigação do aquecimento global?",
                alternativas = new string[] { "Pela liberação de oxigênio que se perde nas camadas superiores da atmosfera", "Pelo armazenamento de bilhões de toneladas de carbono na sua biomassa", "Através da produção de sementes que absorvem calor durante a germinação" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "A vegetação amazônica é classificada como latifoliada e perenifólia. O que esses termos indicam sobre a adaptação das plantas ao meio?",
                alternativas = new string[] { "Perda total das folhas durante a estação seca para economizar energia", "Folhas largas para captar luz e manutenção das folhas durante todo o ano", "Presença de espinhos e cascas grossas para proteção contra incêndios naturais" },
                respostaCorreta = 1,
                dificuldade = Dificuldade.Dificil
            },
            new Pergunta
            {
                enunciado = "Sobre a biodiversidade da fauna amazônica, qual fator abaixo representa um risco crítico causado pela fragmentação da floresta?",
                alternativas = new string[] { "A migração espontânea de todos os animais para biomas vizinhos como o Pampa", "A aceleração da evolução das espécies para se adaptarem ao pasto", "A interrupção de corredores ecológicos, isolando populações de animais" },
                respostaCorreta = 2,
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