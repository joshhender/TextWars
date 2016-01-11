using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    [Range(0, 100)]
    public int health;
    public Slider healthSlider;
    public float jumpForce;
    public LayerMask ground;
    public bool useSecondaryControls;
    public KeyCode[] keys;
    public bool facingRight;
    public GameObject winGO;

    Rigidbody2D _rigidbody;
    float distToGround;
    Collider2D _collider;
    public bool lost;


    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        distToGround = _collider.bounds.extents.y;
        GameManager.instance.players.Add(this);
        if (useSecondaryControls)
        {
            keys[0] = KeyCode.W;
            keys[1] = KeyCode.Space;
            keys[2] = KeyCode.D;
            keys[3] = KeyCode.A;
            facingRight = true;
        }
        else
        {
            keys[0] = KeyCode.UpArrow;
            keys[1] = KeyCode.Return;
            keys[2] = KeyCode.RightArrow;
            keys[3] = KeyCode.LeftArrow;
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector3.up,
            distToGround + 0.1f, ground);
    }

    void Update()
    {
        if (lost)
            return;
        if (Input.GetKeyDown(keys[0]) && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        int horizontal = 0;
        if (Input.GetKey(keys[2]))
        {
            horizontal = 1;
            facingRight = true;
        }
        else if (Input.GetKey(keys[3]))
        {
            horizontal = -1;
            facingRight = false;
        }
        else
            horizontal = 0;

        if(horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if(horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        _rigidbody.velocity = new Vector2(horizontal * speed, _rigidbody.velocity.y);
    }

    public void Win()
    {
        winGO.SetActive(true);
    }
    void Lose()
    {
        GameManager.instance.GameOver(this);
        lost = true;
    }

    public void LoseHealth(int amount)
    {
        if (lost)
            return;
        health -= amount;
        if(health <= 0)
        {
            Lose();
        }
        healthSlider.value = health;
    }
}
