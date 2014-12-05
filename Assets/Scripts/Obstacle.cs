using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour 
{
	private bool [,] mRows;

	void Update()
	{
		Vector3 position = transform.position;
		position.z -= Gameplay.GameDeltaTime * Gameplay.GameSpeed;
		transform.position = position;
	}

	public void SetLogic( bool[,] rows )
	{
		mRows = rows;
	}

	public bool CollideWithPosition( int row, int column )
	{
		return mRows[row,column];
	}

	public override string ToString()
	{
		return string.Format( "[ {0}{1}{2} ] [ {3}{4}{5} ]", mRows[0,0], mRows[0,1], mRows[0,2], mRows[1,0], mRows[1,1], mRows[1,2] );
	}
}
