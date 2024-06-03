using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BriefingPopUp : MonoBehaviour
{
    public static Action nextLevel;
        
    [SerializeField]
    private TextMeshProUGUI _levelScore;
    [SerializeField]
    private Button _button;
    
    private IScoreSystem _scoreSystem;
    
    private void Awake()
    {
        _scoreSystem = ScoreSystem.instance;
        _button.onClick.AddListener(() => nextLevel?.Invoke());
    }

    public void UpdateBriefingInfo()
    {
        UpdateScore();
    }

    private void UpdateScore() => _levelScore.text = ScoreSystem.instance.GetScore().ToString();
}
