using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed, jumpForce;
    public RangeInt health, power;
    public Slider healthSlider, powerSlider;
    public int powerDecrease, powerIncrease;
    public bool useSecondaryControls, facingRight, lost, engaged;
    public KeyCode[] keys;
    public LayerMask ground;
    public GameObject winGO;
    public Animator anim;
    public Weapon weapon;

    Rigidbody2D _rigidbody;
    float distToGround;
    Collider2D _collider;
    SpecialPower SP;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
        anim = GetComponent<Animator>();
        SP = GetComponent<SpecialPower>();
        distToGround = _collider.bounds.extents.y;
        UpdateHealthSlider();
        GameManager.instance.players.Add(this);
        GameManager.instance.SetControls();
        if (useSecondaryControls)
        {
            keys[0] = KeyCode.W;
            keys[1] = KeyCode.Space;
            keys[2] = KeyCode.D;
            keys[3] = KeyCode.A;
            keys[4] = KeyCode.LeftShift;
            facingRight = true;
        }
        else
        {
            keys[0] = KeyCode.UpArrow;
            keys[1] = KeyCode.Return;
            keys[2] = KeyCode.RightArrow;
            keys[3] = KeyCode.LeftArrow;
            keys[4] = KeyCode.RightShift;
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
        {
            weapon.isAttacking = false;
            weapon.col.hasAttacked = true;
            return;
        }
        if (Input.GetKeyDown(keys[0]) && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(keys[1]))
        {
            weapon.Attack(IsGrounded());
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
            weapon.isAttacking = false;
        else
            weapon.isAttacking = true;

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

        if (Input.GetKeyDown(keys[4]) && power.num >= power.max)
        {
            SP.Engage();
        }

        if(horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if(horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        _rigidbody.velocity = new Vector2(horizontal * speed, _rigidbody.velocity.y);
        UpdatePowerSlider(engaged);
    }

    public void UpdateHealthSlider()
    {
        healthSlider.maxValue = health.max;
        healthSlider.minValue = health.min;
        healthSlider.value = health.num;
    }

    public void UpdatePowerSlider(bool inUse)
    {
        if (power.num > power.max)
        {
            power.num = power.max;
            return;
        }
        else if(power.num < power.min)
        {
            power.num = power.min;
            return;
        }

        if (inUse)
        {
            power.num -= powerDecrease * Time.deltaTime;
        }
        else
        {
            power.num += powerIncrease * Time.deltaTime;
        }
        powerSlider.value = power.num;
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

    public void TakeDamage(int amount)
    {
        if (lost)
            return;
        health.num -= amount;
        if(health.num <= 0)
        {
            Lose();
        }
        UpdateHealthSlider();
    }
}
