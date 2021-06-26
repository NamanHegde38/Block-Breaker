using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] private Sprite[] hitSprites;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private AudioClip breakSound;

    [SerializeField] private float volume;
    [SerializeField] private int timesHit;
    
    private Level _level;
    private GameSession _gameSession;
    private SpriteRenderer _spriteRenderer;

    private void Start() {
        _level = FindObjectOfType<Level>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks() {
        _gameSession = FindObjectOfType<GameSession>();
        if (CompareTag("Breakable")) {
            _level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D() {
        if (CompareTag("Breakable")) {
            HandleHit();
        }
    }

    private void HandleHit() {
        timesHit++;
        var maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits) {
            DestroyBlock();
        }
        else {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite() {
        var spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null) {
            _spriteRenderer.sprite = hitSprites[spriteIndex];
        }
        else {
            Debug.Log("Block sprite is missing from array: " + gameObject.name);
        }
    }

    private void DestroyBlock() {
        _gameSession.AddToScore();
        _level.BlockDestroyed();
        PlayBlockDestroySFX();
        TriggerSparklesVFX();
        Destroy(gameObject);
    }

    private void PlayBlockDestroySFX() {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, volume);
    }

    private void TriggerSparklesVFX() {
        var sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2);
    }
}