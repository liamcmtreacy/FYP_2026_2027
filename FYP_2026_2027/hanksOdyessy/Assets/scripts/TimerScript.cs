using UnityEngine;
using TMPro;
using JetBrains.Annotations; // text mesh pro
public class TimerScript : MonoBehaviour
{
    public float timePassed = 0f; // var
    public TextMeshProUGUI timingText;
    // Update is called once per frame
    void Update()
    { //shockingly simple to add 
        timePassed +=Time.deltaTime;
        timingText.text = "Time: " + Mathf.Floor(timePassed).ToString();
    }
}
