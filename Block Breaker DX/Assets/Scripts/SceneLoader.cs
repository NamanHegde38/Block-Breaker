using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [SerializeField] private MMFeedbacks startFeedback, quitFeedback;
    [SerializeField] private MMFeedbacks loadStartFeedback, loadEndFeedback;
    [SerializeField] private MMFeedbacks winFeedback;

    private void Start() {
        loadEndFeedback.Initialization();
        PlayLoadFeedbacks();
    }

    private void PlayLoadFeedbacks() {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (currentSceneIndex == 1) {
            var splashScreen = FindObjectOfType<SplashScreen>();
            if (splashScreen) {
                Destroy(splashScreen);
                startFeedback?.PlayFeedbacks();
            }
            else {
                loadEndFeedback?.PlayFeedbacks();
            }
        }
        else {
            loadEndFeedback?.PlayFeedbacks();
        }
    }
    
    public void LoadNextScene(bool hasWon = false) {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(hasWon ?
            PlayWinFeedback(currentSceneIndex + 1) : 
            LoadScene(currentSceneIndex + 1));
    }

    private IEnumerator PlayWinFeedback(int sceneIndex) {
        winFeedback?.PlayFeedbacks();
        if (winFeedback is { })
            yield return new WaitForSeconds(winFeedback.TotalDuration);
        StartCoroutine(LoadScene(sceneIndex));
    }
    
    public void LoadStartScene() {
        StartCoroutine(LoadScene(1));
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver() {
        StartCoroutine(LoadScene(7));
    }

    private IEnumerator LoadScene(int sceneIndex) {
        loadStartFeedback?.PlayFeedbacks();
        if (loadStartFeedback is { })
            yield return new WaitForSeconds(loadStartFeedback.TotalDuration);
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void QuitGame() {
        StartCoroutine(Quit());
    }

    private IEnumerator Quit() {
        quitFeedback?.PlayFeedbacks();
        if (quitFeedback is { })
            yield return new WaitForSeconds(quitFeedback.TotalDuration);
        Application.Quit();
    }
}
