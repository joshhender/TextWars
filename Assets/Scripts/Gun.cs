using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public int damage;
    public LayerMask playerLayer;
    public Transform firePoint;

    Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        if (player.lost)
            return;
        if (Input.GetKeyDown(player.keys[1]))
        {
            Fire();
        }
    }

    void Fire()
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
            hit.transform.GetComponent<Player>().LoseHealth(damage);
        }
    }
}
