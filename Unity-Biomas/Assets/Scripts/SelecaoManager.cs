using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SelecaoManager : MonoBehaviour {
    
    public TMP_InputField campoNome; 

    [Header("Configuração de Feedback (Antigo/Texto)")]
    public TextMeshProUGUI textoAvisoErro; 

    [Header("PAINEL 1: Nome Repetido")]
    public GameObject painelJogadorRepetido; // Arraste o painel de nome repetido aqui

    [Header("PAINEL 2: Nome em Branco")]
    public GameObject painelNomeEmBranco;    // Arraste o painel de campo vazio aqui

    [Header("PAINEL 3: Voltar")]
    public GameObject painelVoltar;    // Arraste o painel de campo vazio aqui

    [Header("PAINEL 4: Sair")]
    public GameObject painelSair;    // Arraste o painel de campo vazio aqui

    public void SelecionarAvatar(AvatarData dados) {
        if (GameHandler.instance != null) {
            GameHandler.instance.avatarSelecionado = dados;
            Debug.Log("Avatar selecionado: " + dados.nomeAvatar);
        }
    }

    public void AvancarCena() {
        if (campoNome == null) {
            campoNome = GameObject.FindFirstObjectByType<TMP_InputField>();
        }

        if (campoNome == null) {
            Debug.LogError("ERRO: O sistema não encontrou nenhum campo de texto na cena para ler o nome!");
            return;
        }

        string nomeDigitado = campoNome.text.Trim();

        // 1. 🛑 VALIDAÇÃO 1: Verifica se o jogador deixou o campo em branco
        if (string.IsNullOrEmpty(nomeDigitado)) {
            AbrirPainelNomeEmBranco(); // 👈 Abre o painel específico de campo vazio
            return; 
        }

        // 2. 🛑 VALIDAÇÃO 2: Verifica se o nome já existe no histórico do ranking
        if (NomeJaExisteNoRanking(nomeDigitado)) {
            AbrirPainelJogadorRepetido(); // 👈 Abre o painel específico de nome repetido
            return; 
        }

        // 3. Se passou pelas duas validações, salva e avança
        if (GameHandler.instance != null) {
            GameHandler.instance.nomeJogador = nomeDigitado;
            Debug.Log("Nome salvo: " + nomeDigitado);
            SceneManager.LoadScene("SelecaoBioma");
        }
    }

    // ========================================================
    // FUNÇÃO AUXILIAR: Varre o JSON do ranking procurando o nome
    // ========================================================
    private bool NomeJaExisteNoRanking(string nomeParaVerificar) {
        if (!PlayerPrefs.HasKey("Ranking")) {
            return false; 
        }

        try {
            string json = PlayerPrefs.GetString("Ranking");
            Ranking rankingSalvo = JsonUtility.FromJson<Ranking>(json);

            if (rankingSalvo != null && rankingSalvo.jogadores != null) {
                foreach (Jogador j in rankingSalvo.jogadores) {
                    if (j.nome.ToLower() == nomeParaVerificar.ToLower()) {
                        return true; 
                    }
                }
            }
        }
        catch (System.Exception e) {
            Debug.LogError("Erro ao ler o arquivo de Ranking para validação de nome: " + e.Message);
        }

        return false; 
    }

    // ========================================================
    // CONTROLES EXCLUSIVOS DO PAINEL 1: JOGADOR REPETIDO
    // ========================================================
    public void AbrirPainelJogadorRepetido() {
        if (painelJogadorRepetido != null) {
            painelJogadorRepetido.SetActive(true);
        } else {
            ExibirAviso("Este nome já está registrado no Ranking! Escolha outro.");
        }
    }

    public void FecharPainelJogadorRepetido() {
        if (painelJogadorRepetido != null) {
            painelJogadorRepetido.SetActive(false);
        }
    }

    // ========================================================
    // CONTROLES EXCLUSIVOS DO PAINEL 2: NOME EM BRANCO
    // ========================================================
    public void AbrirPainelNomeEmBranco() {
        if (painelNomeEmBranco != null) {
            painelNomeEmBranco.SetActive(true);
        } else {
            ExibirAviso("Por favor, digite o seu nome antes de continuar.");
        }
    }

    public void FecharPainelNomeEmBranco() {
        if (painelNomeEmBranco != null) {
            painelNomeEmBranco.SetActive(false);
        }
    }

     // ========================================================
    // CONTROLES EXCLUSIVOS DO PAINEL 3: VOLTA SELECAOBIOMA
    // ========================================================
    public void AbrirPainelVoltar() {
        if (painelVoltar != null) {
            painelVoltar.SetActive(true);
        } else {
            ExibirAviso("Tem certeza que deseja voltar? Todo seu progresso será perdido.");
        }
    }

    public void FecharPainelVoltar() {
        if (painelVoltar != null) {
            painelVoltar.SetActive(false);
        }
    }

     // ========================================================
    // CONTROLES EXCLUSIVOS DO PAINEL 4: SAIR
    // ========================================================
    public void AbrirPainelSair() {
        if (painelSair != null) {
            painelSair.SetActive(true);
        } else {
            ExibirAviso("Tem certeza que deseja sair? Isso fechará o jogo.");
        }
    }

    public void FecharPainelSair() {
        if (painelSair != null) {
            painelSair.SetActive(false);
        }
    }

    // ========================================================
    // FUNÇÃO AUXILIAR: Texto de fallback (caso falte o objeto no Inspector)
    // ========================================================
    private void ExibirAviso(string mensagem) {
        Debug.LogWarning(mensagem);

        if (textoAvisoErro != null) {
            textoAvisoErro.text = mensagem; 
            textoAvisoErro.gameObject.SetActive(true);
        }
    }
}