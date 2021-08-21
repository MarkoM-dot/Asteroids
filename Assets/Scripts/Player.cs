using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float thrustspeed = 1.0f;
    public float turnspeed = 1.0f;

    // Movement Sprites
    [SerializeField] Sprite thrust;
    [SerializeField] Sprite rest;
    [SerializeField] Sprite rotateLeft;
    [SerializeField] Sprite rotateRight;
    [SerializeField] Sprite thrustRotateRight;
    [SerializeField] Sprite thrustRotateLeft;

    private bool _thrusting;
    private float _turnDirection;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (_thrusting)
            {
                _spriteRenderer.sprite = thrustRotateLeft;
            }
            else
            {
                _spriteRenderer.sprite = rotateLeft;
            }
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (_thrusting)
            {
                _spriteRenderer.sprite = thrustRotateRight;
            }
            else
            {
                _spriteRenderer.sprite = rotateRight;
            }
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
    }
    private void FixedUpdate()
    {
        // We can only thrust upwards
        if (_thrusting)
        {
            _spriteRenderer.sprite = thrust;
            _rigidbody.AddForce(this.transform.up * thrustspeed);
        }
        if (_thrusting == false && _turnDirection == 0.0f)
        {
            _spriteRenderer.sprite = rest;
        }
        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnspeed);
        }
    }
    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0.0f;

            gameObject.SetActive(false);

            // This is an expensive function to use and it is not good practice
            // TODO: let the game manager know the player has died in a better way

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
