using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float JumpHeight = 1.5f;
	public float JumpWidth = 2.5f;

	private float mCentreX;
	private float mTargetX;
	private float mStartX;
	private float mStartY;
	private float mMoveTime;
	private float mJumpTime;
	private bool mJumping;

	private const float MoveDuration = 0.5f;
	private const float JumpDuration = 1.0f;

	public int Column { get; private set; }
	public int Row { get; private set; }

	void Start() 
	{
		MouseInput.OnTap += HandleOnTap;
		MouseInput.OnSwipe += HandleOnSwipe;
		mCentreX = transform.position.x;
		mStartY = transform.position.y;
		mJumpTime = 0.0f;
		mJumping = false;
		Row = 0;
		Column = 1;
	}

	void Update() 
	{
		Vector3 position = transform.position;

		if( mJumping )
		{
			mJumpTime += Time.deltaTime;
			mJumping = ( mJumpTime <= JumpDuration );

			float t;
			float y;

			if( mJumpTime < JumpDuration * 0.5f )
			{
				t = mJumpTime / ( JumpDuration * 0.5f );
				y = Mathf.SmoothStep( mStartY, JumpHeight, t );
				Row = 1;
			}
			else
			{
				t = ( mJumpTime - ( JumpDuration * 0.5f ) ) / ( JumpDuration * 0.5f );
				y = Mathf.SmoothStep( JumpHeight, mStartY, t );
				Row = 0;
			}

			position.y = y;
		}

		if( mStartX != mTargetX )
		{
			mMoveTime += Time.deltaTime;
			float t = mMoveTime / ( MoveDuration );
			float x = Mathf.SmoothStep( mStartX, mTargetX, t );

			position.x = x;
		}

		transform.position = position;
	}

	public void Reset()
	{
		Vector3 position = new Vector3( mCentreX, mStartY, 0.0f );
		transform.position = position;
		mStartX = mTargetX = mCentreX;
		mJumpTime = 0.0f;
		mJumping = false;
		Row = 0;
		Column = 1;
	}

	private void HandleOnTap( Vector3 position )
	{
		Jump();
	}

	private void HandleOnSwipe( MouseInput.Direction direction )
	{
		switch( direction )
		{
		case MouseInput.Direction.Up:
			Jump();
			break;
		case MouseInput.Direction.Left:
			JumpLeft();
			break;
		case MouseInput.Direction.Right:
			JumpRight();
			break;
		}
	}

	private void HandleOnTilt()
	{
	}

	private void Jump()
	{
		if( !mJumping )
		{
			mJumpTime = 0.0f;
			mJumping = true;
		}
	}

	private void JumpLeft()
	{
		if( !mJumping )
		{
			mStartX = transform.position.x;

			if( mStartX > mCentreX )
			{
				mTargetX = mCentreX;
				Column = 1;
			}
			else
			{
				mTargetX = mCentreX - JumpWidth;
				Column = 0;
			}

			mMoveTime = 0.0f;
		}
	}

	private void JumpRight()
	{
		if( !mJumping )
		{
			mStartX = transform.position.x;

			if( mStartX < mCentreX )
			{
				mTargetX = mCentreX;
				Column = 1;
			}
			else
			{
				mTargetX = mCentreX + JumpWidth;
				Column = 2;
			}

			mMoveTime = 0.0f;
		}
	}
}
