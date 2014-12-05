using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gameplay : MonoBehaviour 
{
	public GUIStyle Style;
	public float DistancePastPlayerForDestruction = 10.0f;
	public float DistanceToPlayerForLogic = 0.2f;

	private enum State { TapToStart, Game, GameOver };

	private List<Obstacle> mObstacles;
	private DifficultyCurve mDifficulty;
	private Player mPlayer;
	private State mCurrentState;

	public static float GameDeltaTime { get; private set; }
	public static float GameSpeed { get { return DifficultyCurve.GameSpeed; } }
	public static bool Paused { get; private set; }

	void Awake()
	{
		MouseInput.OnTap += HandleOnTap;
		mObstacles = new List<Obstacle>();
		mDifficulty = GetComponentInChildren<DifficultyCurve>();
		mPlayer = GetComponentInChildren<Player>();
		mCurrentState = State.TapToStart;
		Paused = true;
	}

	void Update()
	{
		GameDeltaTime = Paused ? 0.0f : Time.deltaTime;
		mDifficulty.ProcessNextObstacle( GameDeltaTime );

		if( mDifficulty.NextObstacle != null )
		{
			mObstacles.Add( mDifficulty.NextObstacle );
			mDifficulty.ClearNextObsticale();
		}

		List<Obstacle> oldObstacles = new List<Obstacle>();

		for( int count = 0; count < mObstacles.Count; count++ )
		{
			Obstacle o = mObstacles[count];
			if( o.transform.position.z < -DistancePastPlayerForDestruction )
			{
				oldObstacles.Add( o );
				mObstacles.Remove( o );
			}
			else if( o.transform.position.z < DistanceToPlayerForLogic && o.transform.position.z > -DistanceToPlayerForLogic )
			{
				CollideWithPlayer( o );
			}
		}

		for( int count = 0; count < oldObstacles.Count; count++ )
		{
			Destroy( oldObstacles[count].gameObject ); 
		}
	}

	void OnGUI()
	{
		switch( mCurrentState )
		{
		case State.TapToStart:
			GUI.Box( new Rect( Screen.width * 0.25f, 0, Screen.width * 0.5f, Screen.height * 0.5f ), "Tap to Start", Style );
			break;
		case State.Game:
			GUI.Box( new Rect( 0, 0, Screen.width * 0.5f, Screen.height * 0.2f ), string.Format( "Distance: {0:0.0} m", mDifficulty.Distance ), Style );
			break;
		case State.GameOver:
			GUI.Box( new Rect( Screen.width * 0.25f, 0, Screen.width * 0.5f, Screen.height * 0.5f ), string.Format( "Game Over!  Total Distance: {0:0.0} m", mDifficulty.Distance ), Style );
			break;
		}
	}

	private void CollideWithPlayer( Obstacle o )
	{
		if( o.CollideWithPosition( mPlayer.Row, mPlayer.Column ) )
		{
			Paused = true;
			mCurrentState = State.GameOver;
		}
	}

	private void Reset()
	{
		for( int count = 0; count < mObstacles.Count; count++ )
		{
			Destroy( mObstacles[count].gameObject );
		}

		mObstacles.Clear();

		mPlayer.Reset();
		mDifficulty.Reset();
	}

	private void HandleOnTap( Vector3 position )
	{
		switch( mCurrentState )
		{
		case State.TapToStart:
			Paused = false;
			mCurrentState = State.Game;
			break;
		case State.GameOver:
			Reset();
			mCurrentState = State.TapToStart;
			break;
		}
	}
}
