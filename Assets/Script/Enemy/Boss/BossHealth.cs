using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This is from the tuotorial of Brackeys "How to make a BOSS in Unity!" from youtube
public class BossHealth : MonoBehaviour
{
	public int health = 10;

	public GameObject deathEffect;
	public Slider healthBar;
	public Animator bossAnimaotor;

	public bool isInvulnerable = false;
	public bool bossIsDead = false;

	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;
		
		health -= damage;
		healthBar.value = health;
		Debug.Log(health);

		if (health <= 5)
		{
			GetComponent<Animator>().SetBool("isEnrage", true);
		}

		if (health <= 0)
		{
			Debug.Log("Boss Died");
			bossIsDead = true;
			StartCoroutine(BossDeath());
		}
	}

	// My Changes
	IEnumerator BossDeath()
    {
		yield return new WaitForSecondsRealtime(2f);

		Destroy(gameObject);
    }

}
