using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int pointsPerBlockDestroyed = 83;
    [SerializeField] private bool isAutoPlayEnabled = false;
    
    [SerializeField] private int currentScore;

    private int _currentSceneIndex;
    private Canvas _gameCanvas;
    
    private void Awake() {
        var numberOfObjects = FindObjectsOfType<GameSession>().Length;
        if (numberOfObjects > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start() {
        scoreText.text = currentScore.ToString();
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _gameCanvas = transform.GetChild(0).GetComponent<Canvas>();
    }

    private void Update() {
        if (SceneManager.GetActiveScene().buildIndex == _currentSceneIndex) return;
        _gameCanvas.worldCamera = Camera.main;
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void AddToScore() {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame() {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled() {
        return isAutoPlayEnabled;
    }
}
