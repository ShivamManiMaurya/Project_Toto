using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is from the tuotorial of Brackeys "How to make a BOSS in Unity!" from youtube
public class BossWeapon : MonoBehaviour
{
	public int attackDamage = 1;
	public int enragedAttackDamage = 2;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;
	

	public void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerCollision>().TotoGotHit(attackDamage);
		}
	}

	public void EnragedAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerCollision>().TotoGotHit(enragedAttackDamage);
		}
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
