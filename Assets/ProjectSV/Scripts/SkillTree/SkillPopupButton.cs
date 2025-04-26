using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPopupButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image occluded;
    private Button button;
    private Color originalColor;

    private void Awake()
    {
        button = GetComponent<Button>();
        originalColor = buttonText.color;
    }

    public void ButtonInteractableToggle(bool tf)
    {
        button.interactable = tf;
        occluded.gameObject.SetActive(!tf);
    }

    public void SetText(string _text)
    {
        buttonText.text = _text;
    }

    public void SetTextColorRed()
    {
        buttonText.color = Color.red;
        StopAllCoroutines();
        StartCoroutine(ColorFadeCoroutine(2f));
    }

    private IEnumerator ColorFadeCoroutine(float duration)
    {
        float time = 0f;
        Color startColor = buttonText.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            buttonText.color = Color.Lerp(startColor, originalColor, time / duration);
            yield return null;
        }

        buttonText.color = originalColor;
    }
}
