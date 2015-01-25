using UnityEngine;
using System.Collections;

public class WallResize : MonoBehaviour {

	public enum WallState
	{
		Growing,
		Breathing,
		Shrinking
	}

	public enum BreatheState
	{
		Wait,
		Grow,
		Shrink
	}

	[System.NonSerialized]
	public WallState state = WallState.Growing;

	private static Vector3 initScale;
	private static float currentScale = 1.0f;
	private const float GROWTH_FACTOR = 0.00001f;
	private const float MAX_SCALE = 1.7f;
	private const float MIN_SCALE = 1.0f;

	private bool scaleChangedThisFrame = false;

	private float timer = 0.0f;
	private float breatheWaitTime = 2.0f;
	private float breatheScaleTIme = 1.5f;

	private BreatheState breatheState = BreatheState.Wait;

	void Awake()
	{
		initScale = transform.localScale;
	}

	void Update()
	{
		if(!scaleChangedThisFrame)
		{
			switch(state)
			{
			case WallState.Growing:

				float levelTime = Player.main.GetComponent<PlayerMovement>().floodFade.timeToFlood;

				if(levelTime <= 30.0f)
				{
					state = WallState.Breathing;
				}

				//Debug.Log(timer);
				//Debug.Log((timer - 30.0f) / 30.0f);

				currentScale = Mathf.Lerp(MIN_SCALE, MAX_SCALE, (timer - 30.0f) / 30.0f);
				scaleChangedThisFrame = true;

				break;

			case WallState.Breathing:

				switch(breatheState)
				{
				case BreatheState.Wait:
					timer += Time.deltaTime;

					if(timer >= breatheWaitTime)
					{
						breatheState = BreatheState.Grow;
						timer = 0.0f;
					}

					scaleChangedThisFrame = true;

					break;

				case BreatheState.Grow:
					timer += Time.deltaTime;

					if(timer > breatheScaleTIme)
					{
						breatheState = BreatheState.Shrink;
						timer = 0.0f;
						currentScale = MAX_SCALE;
					}
					else
					{
						currentScale = Mathf.Lerp(MIN_SCALE, MAX_SCALE, timer / breatheScaleTIme);
					}

					scaleChangedThisFrame = true;

					break;

				case BreatheState.Shrink:
					timer += Time.deltaTime;

					if(timer > breatheScaleTIme)
					{
						breatheState = BreatheState.Wait;
						timer = 0.0f;
						currentScale = MIN_SCALE;
					}
					else
					{
						currentScale = Mathf.Lerp(MAX_SCALE, MIN_SCALE, timer / breatheScaleTIme);
					}

					scaleChangedThisFrame = true;
					
					break;
				}

				break;

			case WallState.Shrinking:
				break;
			}

			//Debug.Log(currentScale);
		}

		if(scaleChangedThisFrame)
		{
			transform.localScale = new Vector3(initScale.x * currentScale, initScale.y * currentScale, initScale.z);
		}
	}

	void LateUpdate()
	{
		scaleChangedThisFrame = false;
	}

	public void Reset()
	{
		currentScale = 1.0f;
		state = WallState.Growing;
	}
}