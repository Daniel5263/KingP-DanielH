using TMPro;
using UnityEngine;

public class TimeBehavior : MonoBehaviour{

    private float timer;
    private TextMeshProUGUI textField;

    void Start(){
        textField = GetComponent<TextMeshProUGUI>();

        if (textField == null){
            Debug.Log("No component found.");
        }
    }

    void Update(){
        timer = Time.time;

        textField.text = timer.ToString();

        if(textField != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            string message = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

            textField.SetText(message);
        }

        // Debug.Log(timer);
    }
}
