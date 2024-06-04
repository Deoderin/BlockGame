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
    private CanvasGroup _parentGUIGameObject;

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
        IScoreSystem.scoreChanged += UpdateScore;
        IScoreSystem.multiplierChanged += UpdateMultiplier;
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

    private void UpdateMultiplier()
    {
        ShakeMultiplier();
        UpdateScore();
        _multiplierNumber = _scoreSystem.GetMultiplier();
        _multipiller.text = _multiplierNumber > 0 ? "X" + _multiplierNumber : "";
    }

    private void UpdateScore() => _score.text = _scoreSystem.GetScore().ToString();

    private void ShakeMultiplier()
    {
        float animationTime = 0.5f;
        
        _multipiller.transform.DOShakePosition(animationTime, 5);
        _multipiller.transform.DOShakeRotation(animationTime, 5);
    }

    public void OpenUI()
    {
        _parentGUIGameObject.DOFade(1, 0.4f);
        _parentGUIGameObject.interactable = true;
        _parentGUIGameObject.blocksRaycasts = true;
    }    
    
    public void CloseUI()
    {
        _parentGUIGameObject.DOFade(0, 0.4f);
        _parentGUIGameObject.interactable = false;
        _parentGUIGameObject.blocksRaycasts = false;
        CloseBriefingPop();
    }

    private void OnDestroy()
    {
        BriefingPopUp.nextLevel -= CloseBriefingPop;
        IScoreSystem.scoreChanged -= UpdateScore;
        IScoreSystem.multiplierChanged -= UpdateMultiplier;
        Shape.levelCompleted -= OpenBriefingPop;
    }
}
