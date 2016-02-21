using UnityEngine;
using System.Collections;

public class WeaponCol : MonoBehaviour {

    public bool hasAttacked = false;

	void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag != "Player")
            return;
        if (hasAttacked)
            return;
        transform.parent.GetComponent<Melee>().Hit(col.GetComponent<Player>(), this);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" && hasAttacked)
            hasAttacked = false;
    }
}
