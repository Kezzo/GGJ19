using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : MonoBehaviour 
{
	[SerializeField] private float Damage = 0.1f;

	private Collider2D opponentCollider;
	private Fighter opponent;

	public void OnAttack()
	{
		if( opponent == null )
			return;

		if( opponent.CurrentPose == Pose.BLOCK )
			return;

		var win = opponent.TakeDamage( Damage );

		if( win )
			Debug.Log( "YOU WIN" );
	}

    void OnTriggerEnter2D( Collider2D col )
    {
		Debug.Log( col.gameObject.name + " entered " + gameObject.name + "'s range" );
		opponentCollider = col;
		opponent = col.gameObject.GetComponent<Fighter>();
    }

	void OnTriggerExit2D( Collider2D col )
	{
		Debug.Log( col.gameObject.name + " left from " + gameObject.name + "'s range" );
		if( col == opponentCollider )
		{
			opponentCollider = null;
			opponent = null;
		}
	}
}