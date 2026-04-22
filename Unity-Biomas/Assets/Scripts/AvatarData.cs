using UnityEngine;

[CreateAssetMenu(fileName = "NovoAvatar", menuName = "Jogo/Avatar")]
public class AvatarData : ScriptableObject {
    public int id;
    public string nomeAvatar;
    public Sprite foto;
}