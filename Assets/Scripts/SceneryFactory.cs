// 
// Unity Infinite Runner Project 
// 
// 
// 

using UnityEngine;
using System.Collections;

public class SceneryFactory : MonoBehaviour 
{
	public float TimeBetweenObjects = 1.0f;
	public float RandXStart = 1.0f;
	public float RandXEnd = 1.0f;
	public int NumToCreateOnStart = 60;

	private float mTimeToNextObject;

	void Awake()
	{
		mTimeToNextObject = 0.0f;

		for( int count = 0; count < NumToCreateOnStart; count++ )
		{
			CreateCube( RandXStart, RandXEnd, 0.0f, transform.position.z );
		}
	}

	void Update()
	{
		mTimeToNextObject -= Gameplay.GameDeltaTime;

		if( mTimeToNextObject < 0.0f )
		{
			CreateCube( RandXStart, RandXEnd, transform.position.z, transform.position.z );
			mTimeToNextObject = TimeBetweenObjects;
		}
	}

	private void CreateCube( float x1, float x2, float z1, float z2 )
	{
		float x = UnityEngine.Random.Range( x1, x2 );
		float z = UnityEngine.Random.Range( z1, z2 );
		float scale = 0.1f;
		
		GameObject sceneryItem = GameObject.CreatePrimitive( PrimitiveType.Cube );
		sceneryItem.transform.localScale = new Vector3( scale, scale, scale );
		sceneryItem.transform.parent = transform;
		
		sceneryItem.transform.position = new Vector3( x, 0.0f, z );
		sceneryItem.AddComponent<Scenery>();
	}
}
