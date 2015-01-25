using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour {

	private static ScreenFade instance = null;
	public static ScreenFade main
	{
		get { return instance; }
	}

	public enum FadeState
	{
		Invisible,
		FadingToBlack,
		FadingToWhite,
		Visible,
		FadingToClear
	}

	private float timer = 0.0f;
	private float duration = 2.0f;

	private FadeState state = FadeState.Invisible;
	private Color prevColor;
	private Color targetColor;

	private Image image;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			image = GetComponent<Image>();
		}
		else
		{
			Destroy(this);
		}
	}

	void Update()
	{
		switch(state)
		{
		case FadeState.FadingToBlack:
			timer += Time.deltaTime;

			if(timer < duration)
			{
				float t = timer / duration;
				image.color = Color.Lerp(prevColor, targetColor, t);
			}
			else
			{
				image.color = targetColor;
				state = FadeState.Visible;
			}

			break;

		case FadeState.FadingToWhite:
			timer += Time.deltaTime;

			if(timer < duration)
			{
				float t = timer / duration;
				image.color = Color.Lerp(prevColor, targetColor, t);
			}
			else
			{
				image.color = targetColor;
				state = FadeState.Visible;
			}

			break;

		case FadeState.FadingToClear:
			timer += Time.deltaTime;
            
			if(timer < duration)
			{
				float t = timer / duration;
				image.color = Color.Lerp(prevColor, targetColor, t);
			}
			else
			{
				image.color = targetColor;
				state = FadeState.Visible;
			}
            
        	break;
		}
	}

	public void FadeToBlack(float fadeDuration = 2.0f)
	{
		timer = 0.0f;
		state = FadeState.FadingToBlack;
		prevColor = new Color(image.color.r, image.color.g, image.color.b, image.color.a);
		targetColor = Color.black;
	}

	public void FadeToWhite(float fadeDuration = 2.0f)
	{
		timer = 0.0f;
		state = FadeState.FadingToWhite;
		prevColor = new Color(image.color.r, image.color.g, image.color.b, image.color.a);
		targetColor = Color.white;
	}

	public void FadeToClear(float fadeDuration = 2.0f)
	{
		timer = fadeDuration;
		state = FadeState.FadingToClear;
		prevColor = new Color(image.color.r, image.color.g, image.color.b, image.color.a);
		targetColor = new Color(prevColor.r, prevColor.g, prevColor.b, 0.0f);
	}
}