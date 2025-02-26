using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InvincibilityIconBehavior : MonoBehaviour
{
    private TextMeshProUGUI label;
    private Image overlay;

    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].tag == "Overlay")
            {
                overlay = images[i];
            }
        }
    }

    void Update()
    {
        float timeLeft = PinBehaviour.invincibilityTimeLeft;

        if (timeLeft > 0.0f)
        {
            string message = string.Format("{0:0.0}", timeLeft);
            label.SetText(message);

            float fill = timeLeft / PinBehaviour.invincibilityDuration;
            overlay.fillAmount = fill;
        }
        else
        {
            label.SetText("");
            overlay.fillAmount = 0f;
        }
    }
}
