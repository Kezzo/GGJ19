using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour 
{
	[SerializeField] private float HitboxHeight = 30f;
	[SerializeField] private float HitboxWidth = 0.8f;

	private Vector3 parentScale;

    void Awake()
    {
        BoxCollider2D bc;
        bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        bc.size = new Vector2( HitboxWidth, HitboxHeight );
        bc.isTrigger = true;
		parentScale = transform.parent.localScale;
    }
	
	void OnTriggerExit2D( Collider2D col )
	{
		if( col.gameObject.name == "player1" || col.gameObject.name == "player2" )
		{
			if( col.transform.position.x < transform.position.x )
				transform.parent.localScale = new Vector3( -parentScale.x, parentScale.y, parentScale.z );
			else
				transform.parent.localScale = parentScale;
		}
	}
}