using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    public static GameGUI instance;
    public static Action play;

    [SerializeField]
    private BriefingPopUp _briefingPop;
    [SerializeField]
    private RectTransform _closeBriefingAnchor;
    
    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private GameObject _parentGUIGameObject;

    [SerializeField]
    private TextMeshProUGUI _score;    
    [SerializeField]
    private TextMeshProUGUI _multipiller;

    private IScoreSystem _scoreSystem;
    private float _animationTime = 1;
    private int _multiplierNumber;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RegisterAction();
        
        _scoreSystem = ScoreSystem.instance;
        _briefingPop.transform.position = _closeBriefingAnchor.transform.position;
    }

    private void RegisterAction()
    {
        BriefingPopUp.nextLevel += CloseBriefingPop;
        IScoreSystem.multiplierChanged += UpdateScore;
        Shape.levelCompleted += OpenBriefingPop;
        _playButton.onClick.AddListener(() => play.Invoke());
    }

    private void OpenBriefingPop()
    {
        float centerScreenPosition = Screen.width * 0.5f;
        _briefingPop.transform.DOMoveX(centerScreenPosition, _animationTime);
        _briefingPop.UpdateBriefingInfo();
    }

    public void CloseBriefingPop() => _briefingPop.transform.DOMove(_closeBriefingAnchor.transform.position, _animationTime);

    private void UpdateScore()
    {
        ShakeMultiplier();
        _multiplierNumber = _scoreSystem.GetMultiplier();
        _score.text = _scoreSystem.GetScore().ToString();
        _multipiller.text = _multiplierNumber > 0 ? "X" + _multiplierNumber : "";
    }

    private void ShakeMultiplier()
    {
        float animationTime = 0.5f;
        
        _multipiller.transform.DOShakePosition(animationTime, 5);
        _multipiller.transform.DOShakeRotation(animationTime, 5);
    }

    public void OpenUI()
    {
        _parentGUIGameObject.SetActive(true);
    }    
    
    public void CloseUI()
    {
        _parentGUIGameObject.SetActive(false);
        CloseBriefingPop();
    }

    private void OnDestroy()
    {
        BriefingPopUp.nextLevel -= CloseBriefingPop;
        IScoreSystem.multiplierChanged -= UpdateScore;
        Shape.levelCompleted -= OpenBriefingPop;
    }
}
