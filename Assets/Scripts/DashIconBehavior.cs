using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DashIconBehavior : MonoBehaviour
{
    TextMeshProUGUI label;

    void Start()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        string message = "";
        if (PinBehaviour.cooldown > 0.0) {
            message = string.Format("{0:0.0}", PinBehaviour.cooldown);
        }
        label.SetText(message);

    }
}
