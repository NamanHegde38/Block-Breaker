using UnityEngine;

public class Paddle : MonoBehaviour {

    [SerializeField] private float minX, maxX;
    [SerializeField] private float screenWidthInUnits = 16f;

    private GameSession _gameSession;
    private Ball _ball;
    
    private void Start() {
        _gameSession = FindObjectOfType<GameSession>();
        _ball = FindObjectOfType<Ball>();
    }

    private void Update() {
        var paddlePos =
            new Vector2(transform.position.x, transform.position.y) 
                {x = Mathf.Clamp(GetXPos(), minX, maxX)};
        transform.position = paddlePos;
    }

    private float GetXPos() {
        if (!_ball) return transform.position.x;
        if (_gameSession.IsAutoPlayEnabled()) {
            return _ball.transform.position.x;
        }
        else {
            var mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
            return mousePosInUnits;
        }
    }
}
