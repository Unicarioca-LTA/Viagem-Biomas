using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SelecaoManager : MonoBehaviour {
    // Mantemos como public para você conferir no Inspector
    public TMP_InputField campoNome; 

    [Header("Configuração de Feedback (Opcional)")]
    public TextMeshProUGUI textoAvisoErro; // Arraste um texto aqui se quiser avisar visualmente o jogador

    public void SelecionarAvatar(AvatarData dados) {
        if (GameHandler.instance != null) {
            GameHandler.instance.avatarSelecionado = dados;
            Debug.Log("Avatar selecionado: " + dados.nomeAvatar);
        }
    }

    public void AvancarCena() {
        // Se por algum motivo a referência se perdeu, tentamos encontrar agora
        if (campoNome == null) {
            campoNome = GameObject.FindFirstObjectByType<TMP_InputField>();
        }

        // Se ainda assim for nulo, damos um erro específico
        if (campoNome == null) {
            Debug.LogError("ERRO: O sistema não encontrou nenhum campo de texto na cena para ler o nome!");
            return;
        }

        string nomeDigitado = campoNome.text.Trim();

        // 1. Verifica se o jogador deixou o campo em branco
        if (string.IsNullOrEmpty(nomeDigitado)) {
            ExibirAviso("Por favor, digite o seu nome antes de continuar.");
            return; // 🛑 Bloqueia o avanço
        }

        // 2. 🚀 NOVO BLOQUEIO: Verifica se o nome já existe no histórico do ranking
        if (NomeJaExisteNoRanking(nomeDigitado)) {
            ExibirAviso("Este nome já está registrado no Ranking! Escolha outro.");
            return; // 🛑 Bloqueia o avanço para não duplicar ou misturar dados
        }

        // 3. Se passou pelas validações, salva e avança
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
        // Se não existir nenhuma chave de ranking ainda, o nome está liberado
        if (!PlayerPrefs.HasKey("Ranking")) {
            return false; 
        }

        try {
            string json = PlayerPrefs.GetString("Ranking");
            Ranking rankingSalvo = JsonUtility.FromJson<Ranking>(json);

            if (rankingSalvo != null && rankingSalvo.jogadores != null) {
                // Percorre a lista de jogadores ignorando maiúsculas e minúsculas
                foreach (Jogador j in rankingSalvo.jogadores) {
                    if (j.nome.ToLower() == nomeParaVerificar.ToLower()) {
                        return true; // Encontrou um nome igual!
                    }
                }
            }
        }
        catch (System.Exception e) {
            Debug.LogError("Erro ao ler o arquivo de Ranking para validação de nome: " + e.Message);
        }

        return false; // Nome está limpo e disponível
    }

    // ========================================================
    // FUNÇÃO AUXILIAR: Centraliza a exibição de logs e textos de erro
    // ========================================================
    private void ExibirAviso(string mensagem) {
        Debug.LogWarning(mensagem);

        if (textoAvisoErro != null) {
            textoAvisoErro.text = mensagem;
            textoAvisoErro.gameObject.SetActive(true);
        }
    }
}