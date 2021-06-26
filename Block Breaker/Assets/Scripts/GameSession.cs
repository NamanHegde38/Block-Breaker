using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Range(0.1f, 10f)] [SerializeField] private float gameSpeed = 1f;
    [SerializeField] private int pointsPerBlockDestroyed = 83;
    [SerializeField] private bool isAutoPlayEnabled = false;
    
    [SerializeField] private int currentScore;

    private void Start() {
        scoreText.text = currentScore.ToString();
    }

    private void Update() {
        Time.timeScale = gameSpeed;
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
