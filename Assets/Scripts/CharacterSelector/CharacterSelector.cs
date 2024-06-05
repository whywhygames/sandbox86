using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private PlayerBootstrap _playerBootstrap;

    public void OnClickHandler(CharacterType characterType)
    {
        _playerBootstrap.SetCharater(characterType);   
    }
}
