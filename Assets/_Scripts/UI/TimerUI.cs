using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timerTextMeshProUGUI;
        
        public void SetTime(string getGameTime)
        {
            timerTextMeshProUGUI.text = getGameTime;
        }
    }
}