using UnityEngine;
using TMPro;

public class NicknameUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInput;
    [SerializeField] private TMP_Text resultText;

    public void ShowNickname()
    {
        string nickname = nicknameInput.text;
        resultText.text = nickname;
    }
}