using UnityEngine;
using System.Collections;

public class ObstacleFactory : MonoBehaviour 
{
	public float StartX;
	public float StartY;
	public float ScaleX = 1.0f;
	public float ScaleY = 1.0f;
	public float ScaleZ = 1.0f;

	private static ObstacleFactory mInstance; 

	void Awake()
	{
		if( mInstance == null )
		{
			mInstance = this;
		}
		else
		{
			Debug.LogError( "Only one ObstacleFactory allowed - destorying duplicate" );
			Destroy( this.gameObject );
		}
	}

	public static Obstacle Create( bool [,] rows )
	{
		float x = mInstance.StartX;
		float y = mInstance.StartY;
		float xs = mInstance.ScaleX;
		float ys = mInstance.ScaleY;
		float zs = mInstance.ScaleZ;

		GameObject newObstacle = new GameObject( "Obstacle" );

		for( int column = 0; column < 3; column++ )
		{
			if( rows[0,column] )
			{
				CreateCube( x, y, xs, ys, zs, newObstacle );
			}
			
			x += xs;
		}
		
		x = mInstance.StartX;
		y += ys;

		for( int column = 0; column < 3; column++ )
		{
			if( rows[1,column] )
			{
				CreateCube( x, y, xs, ys, zs, newObstacle );
			}

			x += xs;
		}

		newObstacle.transform.position = new Vector3( 0.0f, 0.0f, mInstance.transform.position.z );

		Obstacle ob = newObstacle.AddComponent<Obstacle>();
		ob.SetLogic( rows );
		return ob;
	}

	private static void CreateCube( float x, float y, float xs, float ys, float zs, GameObject parent )
	{
		GameObject cube = GameObject.CreatePrimitive( PrimitiveType.Cube );
		cube.transform.position = new Vector3( x, y, 0.0f );
		cube.transform.localScale = new Vector3( xs, ys, zs );
		cube.transform.parent = parent.transform;
	}
}
