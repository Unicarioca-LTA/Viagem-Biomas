using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SelecaoManager : MonoBehaviour {
    // Mantemos como public para você conferir no Inspector
    public TMP_InputField campoNome; 

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

        // Verifica se o jogador digitou algo
        if (!string.IsNullOrEmpty(campoNome.text)) {
            GameHandler.instance.nomeJogador = campoNome.text;
            Debug.Log("Nome salvo: " + campoNome.text);
            SceneManager.LoadScene("SelecaoBioma");
        } else {
            Debug.LogWarning("Por favor, digite o seu nome antes de continuar.");
        }
    }
}