using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public int damage;
    public LayerMask playerLayer;
    public Player player;
    public Animator anim;
    public bool isAttacking;
    public WeaponCol col;

    void Start ()
    {
        player = transform.parent.GetComponentInParent<Player>();
        anim = player.GetComponent<Animator>();
        StartUp();
    }

    protected virtual void StartUp() { }

    void Update()
    {
        if (player.lost)
            return;
    }

    public void Attack(bool onGround)
    {
        OnAttack(onGround);
    }

    protected virtual void OnAttack(bool onGround) { }
}
