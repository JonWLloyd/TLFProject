using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class DifficultyCurve : MonoBehaviour 
{

	[Serializable]
	public class DifficultyLevel
	{
		public bool [,] Rows;
		public string Name;

		public void Setup( string n, bool [,] rows )
		{
			Name = n;
			Rows = rows;
		}
	}
	
	private DifficultyLevel [] mLevels;
	public float GameStartSpeed = 10f;
	public float GameSpeedRamp = 0.1f;
	public float TimeBetweenObsticales = 5.0f;
	public float TimeBetweenObsticalesRamp = 0.1f;
	public float HardDistance = 100.0f;

	private LevelSelect levels = DifficultyCurveEditor.instance.returnLevel ();

	private float mTimeBetweenObsticales;
	private float mTimeToNextObsticale;
	private float mDistance;
	private int mNextDifficulty;
	private bool [][,] difficulties;

	public Obstacle NextObstacle { get; private set; }
	public float Distance { get { return mDistance; } }
	public static float GameSpeed { get; private set; }

	void Awake()
	{
		Reset();

		if (levels != null) {

			for (int i = 0; i < levels.LevelList.Count; i++) {

			bool [,] newDiff = {{ levels.LevelList [i].bottomLeft, levels.LevelList [i].bottomMiddle, levels.LevelList [i].bottomRight },{ levels.LevelList[i].topLeft, levels.LevelList[i].topMiddle, levels.LevelList[i].topRight } };

			difficulties[i] = newDiff;
		}


		/*
		bool [,] difficulty1 = { { false, true, false }, { false, false, false } };
		bool [,] difficulty2 = { { true, true, false }, { false, false, false } };
		bool [,] difficulty3 = { { true, true, false }, { false, false, false } };
		bool [,] difficulty4 = { { true, true, false }, { false, true, false } };
		bool [,] difficulty5 = { { true, true, true }, { false, false, false } };
		bool [,] difficulty6 = { { true, true, true }, { false, true, false } };
		bool [,] difficulty7 = { { true, true, true }, { true, false, false } };
		bool [,] difficulty8 = { { true, true, true }, { true, false, true } };
		bool [,] difficulty9 = { { true, true, true }, { true, true, false } };
		bool [,] difficulty10 = { { true, true, true }, { false, true, true } };

		bool [] [,] difficulties = { difficulty1, difficulty2, difficulty3, difficulty4, difficulty5, difficulty6, difficulty7, difficulty8, difficulty9, difficulty10 };

		*/

			mLevels = new DifficultyLevel[levels.LevelList.Count];
			for( int count = 0; count < mLevels.Length; count++ )
			{
				mLevels[count] = new DifficultyLevel();
				mLevels[count].Setup( "", difficulties[count] ); 
			}

		}
	}

	public void ProcessNextObstacle( float dt )
	{
		mDistance += ( GameSpeed * dt );

		if( NextObstacle == null )
		{
			mTimeToNextObsticale -= dt;
			if( mTimeToNextObsticale < 0.0f )
			{
				mTimeBetweenObsticales -= TimeBetweenObsticalesRamp;
				mTimeToNextObsticale = mTimeBetweenObsticales;

				bool LevelChosen = false;

				while(LevelChosen == false){

					System.Random rnd = new System.Random();
					int check = rnd.Next (0,levels.LevelList.Count);

					if (levels.LevelList[check].Start >= GameSpeed){
					
						LevelChosen = true;
						mNextDifficulty = check;
					
					}

				}

				Obstacle o = ObstacleFactory.Create( mLevels[mNextDifficulty].Rows );
				o.transform.parent = transform;

				NextObstacle = o;
				GameSpeed += GameSpeedRamp;

				//float diff = mDistance / HardDistance;
				//mNextDifficulty = (int)( diff * (float)mLevels.Length );
				//mNextDifficulty = Mathf.Clamp( mNextDifficulty, 0, mLevels.Length - 1 );
			}
		}
	}

	public void Reset()
	{
		GameSpeed = GameStartSpeed;
		mTimeBetweenObsticales = TimeBetweenObsticales;
		mTimeToNextObsticale = 0.0f;
		NextObstacle = null;
		mDistance = 0.0f;
		mNextDifficulty = 0;
	}

	public void ClearNextObsticale()
	{
		NextObstacle = null;
	}
}
