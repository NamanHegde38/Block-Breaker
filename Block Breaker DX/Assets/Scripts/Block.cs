using System;
using UnityEngine;
using MoreMountains.Feedbacks;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour {

    [SerializeField] private Sprite[] hitSprites;
    [SerializeField] private GameObject[] blockSparklesVFX;
    [SerializeField] private GameObject blockHitVFX;

    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip breakSound;

    [SerializeField] private float hitVolume;
    [SerializeField] private float breakVolume;
    [SerializeField] private int timesHit;

    [SerializeField] private Vector2 vfxOffset;

    [SerializeField] private MMFeedbacks hitFeedback;
    [SerializeField] private MMFeedbacks breakFeedback;

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
            hitFeedback?.PlayFeedbacks();
            AudioSource.PlayClipAtPoint(hitSound, Vector2.zero, hitVolume);
            var sparkles = Instantiate(blockHitVFX, 
                new Vector3(transform.position.x + vfxOffset.x, transform.position.y + vfxOffset.y, transform.position.z + 5), transform.rotation);
            Destroy(sparkles, 2);
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
        PlayBlockDestroySfx();
        TriggerSparklesVFX();
        breakFeedback?.PlayFeedbacks();
        Destroy(gameObject);
    }

    private void PlayBlockDestroySfx() {
        AudioSource.PlayClipAtPoint(breakSound, Vector2.zero, breakVolume);
    }
    

    private void TriggerSparklesVFX() {
        var effects = blockSparklesVFX[Random.Range(0, blockSparklesVFX.Length)];
        var sparkles = Instantiate(effects, (Vector2)transform.position + vfxOffset, transform.rotation);
        Destroy(sparkles, 2);
    }
}