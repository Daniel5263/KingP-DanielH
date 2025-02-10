using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DashIconBehavior : MonoBehaviour
{
    TextMeshProUGUI label;
    public float fill;
    Image Overlay;

    void Start()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].tag == "Overlay")
            {
                Overlay = images[i];
            }
        }
        fill = 0.0f;
    }

    void Update()
    {
        string message = "";
        if (PinBehaviour.cooldown > 0.0) {
            message = string.Format("{0:0.0}", PinBehaviour.cooldown);
            fill = PinBehaviour.cooldown / PinBehaviour.cooldownRate;
            Overlay.fillAmount = fill;
        }
        label.SetText(message);

    }
}
