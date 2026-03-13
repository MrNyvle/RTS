using UnityEngine;
using UnityEngine.UI;

public class BuildTimerUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    private bool _isBuilding;

    private float _countdownTimer;
    private float _currentTimer;

    void Update()
    {
        this.gameObject.SetActive(_isBuilding);
        if (!_isBuilding)
            return;

        _currentTimer += Time.deltaTime;
        _image.fillAmount = _currentTimer / _countdownTimer;

        if (_currentTimer >= _countdownTimer)
            StopCountdown();
    }

    public void ActivateTimer(float countdownTime)
    {
        _isBuilding = true;
        _image.fillAmount = 0;
        _countdownTimer = countdownTime;
        _currentTimer = 0;
        this.gameObject.SetActive(true);
    }

    public void StopCountdown()
    {
        _isBuilding = false;
    }
}
