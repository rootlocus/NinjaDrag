using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winLabel;
    [SerializeField] private TextMeshProUGUI startGameLabel;
    [SerializeField] private TextMeshProUGUI friendCounter;

    private IEnumerator FadeOutText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }

    public void UpdateFriendCounter(int count) {
        friendCounter.SetText("FRIENDS LEFT: " + count);
    }

    public void ToggleWinLabel() {
        winLabel.enabled = !winLabel.enabled;
    }

    public void ShowStartLabel() {
        StartCoroutine(FadeOutText(0.25f, startGameLabel));
    }
    
}
