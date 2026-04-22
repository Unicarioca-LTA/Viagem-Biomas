using UnityEngine;
using UnityEngine.UI;
using TMPro; // Caso use TextMeshPro

public class ResultadoManager : MonoBehaviour {
    private TextMeshProUGUI textoNomeResultado;
    private Image imagemAvatarResultado;

    void Start() {
        // BUSCA AUTOMÁTICA: Procura na cena objetos com esses nomes específicos
        // Certifique-se de que os nomes na Hierarquia sejam exatamente estes:
        GameObject objTexto = GameObject.Find("NomeDoJogador_Texto");
        GameObject objImagem = GameObject.Find("Avatar_Imagem");

        if (objTexto != null) textoNomeResultado = objTexto.GetComponent<TextMeshProUGUI>();
        if (objImagem != null) imagemAvatarResultado = objImagem.GetComponent<Image>();

        // Lógica de preenchimento (igual antes)
        if (GameHandler.instance != null) {
            if (textoNomeResultado != null)
                textoNomeResultado.text = GameHandler.instance.nomeJogador;
            
            if (imagemAvatarResultado != null && GameHandler.instance.avatarSelecionado != null)
                imagemAvatarResultado.sprite = GameHandler.instance.avatarSelecionado.foto;
        }
    }
}