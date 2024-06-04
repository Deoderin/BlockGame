using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour, IScoreSystem
{
    //ToDo config
    [SerializeField]
    private int _speedScoreBonus;
    [SerializeField]
    private int _gateScore;
    [SerializeField]
    private int _finishScore;
    
    public static ScoreSystem instance;
    
    private int _score;
    private int _multiplier = 1;
    
    private void Awake() //ToDo Inject
    {
        instance = this;
    }

    public int GetScore() => _score;

    public void SetScore(ScoreType score)
    {
        _score += GetScoreWithType(score) * _multiplier;
        IScoreSystem.scoreChanged?.Invoke();
    }

    public void ClearScore()
    {
        _score = 0;
        
        IScoreSystem.scoreChanged?.Invoke();
    }

    public void SetMultiplier(int number)
    {
        _multiplier = number;
        IScoreSystem.multiplierChanged?.Invoke();
    }

    public int GetMultiplier() => _multiplier;

    private int GetScoreWithType(ScoreType score)
    {
        return score switch
        {
            ScoreType.speedScore => _speedScoreBonus,
            ScoreType.gateScore => _gateScore,
            ScoreType.finishScore => _finishScore,
            _ => throw new ArgumentOutOfRangeException(nameof(score), score, null)
        };
    }
}

public interface IScoreSystem
{
    public static Action scoreChanged;      
    public static Action multiplierChanged;    

    int GetScore();
    void SetScore(ScoreType score);
    void ClearScore();
    void SetMultiplier(int number);
    int GetMultiplier();
}

public enum ScoreType
{
    speedScore,
    gateScore,
    finishScore
}