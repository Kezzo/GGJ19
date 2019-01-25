using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour 
{
	private Vector3 parentScale;

    void Awake()
    {
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