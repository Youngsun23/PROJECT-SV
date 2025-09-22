using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDBarPanel : MonoBehaviour
{

    [SerializeField] private Image HPBar;
    // [SerializeField] private TextMeshProUGUI HPTXT;
    [SerializeField] private Image StaminaBar;
    // [SerializeField] private TextMeshProUGUI StaminaTXT;

    public void UpdateHUDUI()
    {

    }

    public void UpdateHUDUIHP(float maxHP, float curHP) // cur/max
    {
        Debug.Log($"{nameof(UpdateHUDUIHP)} »£√‚");

        float targetFill = curHP / maxHP;
        targetFill = Mathf.Clamp01(targetFill);
        StartCoroutine(FillBar(targetFill, HPBar));
        // HPTXT.text = curHP.ToString() + " / " + maxHP.ToString();
    }

    public void UpdateHUDUIStamina(float maxStamina, float curStamina) // cur/max
    {
        Debug.Log($"{nameof(UpdateHUDUIStamina)}");

        float targetFill = curStamina / maxStamina;
        targetFill = Mathf.Clamp01(targetFill);
        StartCoroutine(FillBar(targetFill, StaminaBar));
        // StaminaTXT.text = curStamina.ToString() + " / " + maxStamina.ToString();
    }

    private IEnumerator FillBar(float targetFill, Image bar)
    {
        float duration = 2f;
        float elapsed = 0f;
        float startFill = bar.fillAmount;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            bar.fillAmount = Mathf.Lerp(startFill, targetFill, t);
            yield return null;
        }

        bar.fillAmount = targetFill;
    }
}
