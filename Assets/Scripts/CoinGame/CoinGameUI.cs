using System.Collections;
using UnityEngine;

public class CoinGameUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI nameText;

    public Color damageTextColor;
    public Color normalTextColor;

    private int lastHealth;

    private bool CR_running = false;

    // Start is called before the first frame update
    void Start()
    {
        healthText.color = normalTextColor;
        lastHealth = 1;
    }

    private IEnumerator PulseTextColor(TMPro.TextMeshProUGUI text, Color colorFrom, Color colorTo)
    {
        CR_running = true;
        Vector2 anchoredPosition = healthText.rectTransform.anchoredPosition;

        int totalFrames = 10;
        for (int n = 0; n < totalFrames; n++)
        {
            text.color = Color.Lerp(colorFrom, colorTo, n/(float)totalFrames);

            Vector2 tweak = anchoredPosition + new Vector2(Random.Range(-10f, 10f), Random.Range(-5f, 5f));

            healthText.rectTransform.anchoredPosition = tweak;
            yield return null;
        }

        healthText.rectTransform.anchoredPosition = anchoredPosition;
        CR_running = false;
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = "Code name: " + CoinPlayer.instance?.behavior?.body?.coinName ?? "";

        int health = CoinPlayer.instance.Health;
        decimal healthCents = health;
        healthCents /= 100;
        healthText.text = string.Format("Your Value: ${0:#0.00}", healthCents);

        if (lastHealth > health && !CR_running)
        {
            StartCoroutine(PulseTextColor(healthText, damageTextColor, normalTextColor));
        }

        lastHealth = health;
    }
}
