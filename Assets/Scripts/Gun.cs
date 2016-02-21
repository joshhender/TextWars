using UnityEngine;
using System.Collections;

public class Gun : Weapon {

    public Transform firePoint;

    protected override void OnAttack(bool OnGround)
    {
        Vector2 rayDir;
        if (player.facingRight)
            rayDir = new Vector2(1, 0);
        else
            rayDir = new Vector2(-1, 0);

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, rayDir,
            50, playerLayer);
        if (hit && hit.transform.tag == "Player")
        {
            hit.transform.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
