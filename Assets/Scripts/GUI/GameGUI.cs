using System;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    public static GameGUI instance;
    public static Action play;
    
    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private GameObject _parentGUIGameObject;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RegisterAction();
    }

    private void RegisterAction()
    {
        _playButton.onClick.AddListener(() => play.Invoke());
    }

    public void OpenUI()
    {
        _parentGUIGameObject.SetActive(true);
    }    
    
    public void CloseUI()
    {
        _parentGUIGameObject.SetActive(false);
    }
}
