using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField] private int breakableBlocks;

    [SerializeField] private GameObject winEffect;
    [SerializeField] private GameObject winParticles;

    private SceneLoader _sceneLoader;
    private Ball _ball;

    private void Start() {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _ball = FindObjectOfType<Ball>();
    }

    public void CountBlocks() {
        breakableBlocks++;
    }

    public void BlockDestroyed() {
        breakableBlocks--;

        if (breakableBlocks > 0) return;
        TriggerSparklesVFX(winEffect);
        TriggerSparklesVFX(winParticles);
        _sceneLoader.LoadNextScene(true);
    }
    
    private void TriggerSparklesVFX(GameObject prefab) {
        var sparkles = Instantiate(prefab, _ball.transform.position, transform.rotation);
        Destroy(_ball.gameObject);
        Destroy(sparkles, 2);
    }
}
