using UnityEngine;
using System.Collections;

public class Scenery : MonoBehaviour 
{
	private const float KillMe = -10.0f;

	void Update () 
	{
		Vector3 position = transform.position;
		position.z -= Gameplay.GameDeltaTime * Gameplay.GameSpeed;
		transform.position = position;

		if( transform.position.z < KillMe )
		{
			Destroy( gameObject );
		}
	}
}
