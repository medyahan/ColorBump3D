using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // INSTANCE
    public static GameManager Instance;

    [Header("LEVELS")]
    [SerializeField] private List<GameObject> _levels = new List<GameObject>();
    private int _levelNo;
    public int LevelNo
    {
        get => PlayerPrefs.GetInt("LevelID", 1);
        set => PlayerPrefs.SetInt("LevelID", value);
    }

    [Header("GAME")]
    public static bool IsGameStarted;
    public static bool IsCompleted;
    public static bool IsFailed;

    [Header("PLAYER")] 
    [SerializeField] private PlayerController _player;
    
    [Header("FINISH")] 
    [SerializeField] private GameObject _finish;
    
    //[Header("GAMEPLAY")] 
    //[SerializeField] private GameObject _finish;
    
    // SCORE
    public int LevelScore
    {
        get;
        set;
    }
    private int _totalGameScore;
    public int TotalScoreGame => PlayerPrefs.GetInt("TotalGameScore", 0);

    // CAMERA
    [SerializeField] private CameraController _cameraControl;
    public CameraController CameraControl => _cameraControl;
    
    private void Awake()
    {
        Instance = this;
        
        IsFailed = false;
        IsCompleted = false;
        
        //_levelNo = PlayerPrefs.GetInt("LevelID", 1);
        LoadLevel(LevelNo);
       
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsFailed && !IsCompleted)
        {
            IsGameStarted = true;
            UIManager.Instance.StartGame();
        }
            
    }

    public void OnLevelCompleted()
    {
        if (IsGameStarted)
        {
            IsCompleted = true;
            //SetTotalGameScore();
            
            //UIManager.Instance.Invoke(nameof(UIManager.OpenWinPanel), 2f);
            
            IsGameStarted = false;
            
            Invoke("NextLevel", 2f);
        }
    }

    public void OnLevelFailed()
    {
        if (IsGameStarted)
        {
            IsFailed = true;
            
            //UIManager.Instance.Invoke(nameof(UIManager.OpenFailPanel), 1f);
            
            IsGameStarted = false;
        }
    }
    
    private void LoadLevel(int levelCounter)
    {
        foreach (var level in _levels)
        {
            level.SetActive(false);
        }
        _levels[levelCounter - 1].SetActive(true);
    }

    public void NextLevel()
    {
        if (LevelNo == _levels.Count) // son levelda ise
        {
            LevelNo = 1; // ilk level numarasi
            //PlayerPrefs.SetInt("LevelID", _levelNo);
        }
        else
        {
            LevelNo++;
            //PlayerPrefs.SetInt("LevelID", _levelNo);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int score)
    {
        LevelScore += score;
    }

    private void SetTotalGameScore()
    {
        _totalGameScore = TotalScoreGame;
        _totalGameScore += LevelScore;
        PlayerPrefs.SetInt("TotalGameScore", _totalGameScore);
    }

}
