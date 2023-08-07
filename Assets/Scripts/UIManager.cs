using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // INSTANCE
    public static UIManager Instance;

    [Header("GAME PANEL")] 
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _hand;
    [SerializeField] private TMP_Text _currenLevelText;
    [SerializeField] private TMP_Text _nextLevelText;
    [SerializeField] private Image _progressFill;
    [SerializeField] private TMP_Text _levelNoText;

    private float _startDistance;
    private float _distance;

    [Header("WIN PANEL")] 
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TMP_Text _winLevelScoreText;
    [SerializeField] private TMP_Text _winTotalGameScoreText;

    [Header("FAIL PANEL")] 
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private TMP_Text _failTotalGameScoreText;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        ResetUI();

        _levelNoText.text = "LEVEL " + GameManager.Instance.LevelNo;
        _currenLevelText.text = GameManager.Instance.LevelNo.ToString();
        _nextLevelText.text = (GameManager.Instance.LevelNo + 1).ToString();
        
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.IsGameStarted ) // IF CLICKED AND STARTED
        {
            //StartGame();
        }
    }

    private void ResetUI()
    {
        _gamePanel.SetActive(true);
        //_winPanel.SetActive(false);
        //_failPanel.SetActive(false);
    }

    public void OpenWinPanel()
    {
        _gamePanel.SetActive(false);
        _winPanel.SetActive(true);

        _winLevelScoreText.text = "" + GameManager.Instance.LevelScore;
        _winTotalGameScoreText.text = "" + GameManager.Instance.TotalScoreGame;
    }

    public void OpenFailPanel()
    {
        _gamePanel.SetActive(false);
        _failPanel.SetActive(true);
        
        _failTotalGameScoreText.text = "" + GameManager.Instance.TotalScoreGame;
    }

    public void StartGame()
    {
        _hand.SetActive(false);
    }
}
