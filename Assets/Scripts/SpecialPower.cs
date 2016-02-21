using UnityEngine;
using System.Collections;

public class SpecialPower : MonoBehaviour {

    public float duration;
    public int extraHealth, extraDamage;
    public float extraSpeed;
    public string animBool;

    Player player;
    int originalHealth, originalDamage;
    float originalSpeed;

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void Engage()
    {
        StartCoroutine(Timer());
        originalHealth = (int)player.health.num;
        player.health.max += extraHealth;
        player.health.num = player.health.max;
        player.UpdateHealthSlider();
        originalDamage = player.weapon.damage;
        player.weapon.damage += extraDamage;
        originalSpeed = player.speed;
        player.speed += extraSpeed;
        player.anim.SetBool(animBool, true);
        player.engaged = true;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(duration);
        Disengage();
    }

    void Disengage()
    {
        player.health.num = originalHealth;
        player.health.max -= extraHealth;
        player.UpdateHealthSlider();
        player.weapon.damage = originalDamage;
        player.speed = originalSpeed;
        player.anim.SetBool(animBool, false);
        player.engaged = false;
    }
}
