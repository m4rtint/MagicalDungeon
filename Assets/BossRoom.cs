using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour {

    [SerializeField]
    GameObject boss;

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject entered = col.gameObject;
        if (entered.tag == Tags.PLAYER && !boss.GetComponent<BossController>().awakened)
        {
            boss.GetComponent<BossController>().awakened = true;
            boss.GetComponent<BossController>().healthBar.SetActive(true);
        }
    }
}
