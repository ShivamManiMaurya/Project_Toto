using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
			//Die();
			Debug.Log("Boss Died");
			bossIsDead = true;
			//bossAnimaotor.SetTrigger("Dying");
			
			StartCoroutine(BossDeath());
		}
	}

	//void Die()
	//{
	//	Instantiate(deathEffect, transform.position, Quaternion.identity);
	//	Destroy(gameObject);
	//}
	IEnumerator BossDeath()
    {
		yield return new WaitForSecondsRealtime(2f);

		Destroy(gameObject);
    }

}
