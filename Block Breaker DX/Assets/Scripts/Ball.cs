using UnityEngine;

public class Ball : MonoBehaviour {
     
     [SerializeField] private Paddle paddle;
     [SerializeField] private Vector2 push;
     [SerializeField] private AudioClip[] ballSounds;
     [SerializeField] private float randomFactor = 0.2f;
     [SerializeField] private float soundRandomFactor = 0.2f;
     
     [SerializeField] private GameObject ballHitVFX;

     private Rigidbody2D _rigidbody;
     private AudioSource _audio;

     private bool _hasStarted;
     private Vector2 _paddleToBallVector;
     private float _currentPitch;

     private void Start() {
          _rigidbody = GetComponent<Rigidbody2D>();
          _audio = GetComponent<AudioSource>();
          _paddleToBallVector = transform.position - paddle.transform.position;
          _currentPitch = _audio.pitch;
     }

     private void Update() {
          if (_hasStarted) return;
          LockBallToPaddle();
          LaunchOnMouseClick();
     }
     
     private void LockBallToPaddle() {
          var paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
          transform.position = paddlePos + _paddleToBallVector; 
     }
     
     private void LaunchOnMouseClick() {
          if (!Input.GetMouseButtonDown(0)) return;
          _rigidbody.velocity = new Vector2(Random.Range(-push.x, push.x), push.y);
          _hasStarted = true;
     }

     private void OnCollisionEnter2D() {
          var velocityTweak = new Vector2
               (Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
          
          if (!_hasStarted) return;
          var clip = ballSounds[Random.Range(0, ballSounds.Length)];
          var sparkles = Instantiate(ballHitVFX, transform.position, transform.rotation);
          Destroy(sparkles, 2);
          _audio.pitch = Random.Range(_currentPitch - soundRandomFactor, _currentPitch + soundRandomFactor);
          _audio.PlayOneShot(clip);
          _rigidbody.velocity += velocityTweak;

     }
}
