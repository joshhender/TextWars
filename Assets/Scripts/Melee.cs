using UnityEngine;
using System.Collections;

public class Melee : Weapon {

    protected override void StartUp()
    {
        col = GetComponentInChildren<WeaponCol>();
    }

    protected override void OnAttack(bool onGround)
    {
        if (onGround)
            anim.SetTrigger("Attack");
        else
            anim.SetTrigger("SpinAttack");
        isAttacking = true;
        col.hasAttacked = false;
    }

    public void Hit(Player _player, WeaponCol _col)
    {
        if(isAttacking)
        {
            _player.TakeDamage(damage);
            _col.hasAttacked = true;
        }
    }
}
