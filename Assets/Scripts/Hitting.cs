using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : MonoBehaviour 
{
	[SerializeField] private float HitboxSize = 1.3f;
	[SerializeField] private float Damage = 0.1f;

	private Collider2D opponentCollider;
	private Fighter opponent;

    void Awake()
    {
        BoxCollider2D bc;
        bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        bc.size = new Vector2( HitboxSize, HitboxSize );
        bc.isTrigger = true;
    }

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