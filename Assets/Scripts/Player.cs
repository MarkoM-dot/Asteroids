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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (_thrusting)
            {
                GetComponent<SpriteRenderer>().sprite = thrustRotateLeft;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = rotateLeft;
            }
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (_thrusting)
            {
                GetComponent<SpriteRenderer>().sprite = thrustRotateRight;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = rotateRight;
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
            GetComponent<SpriteRenderer>().sprite = thrust;
            _rigidbody.AddForce(this.transform.up * thrustspeed);
        }
        if (_thrusting == false && _turnDirection == 0.0f)
        {
            GetComponent<SpriteRenderer>().sprite = rest;
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
}
