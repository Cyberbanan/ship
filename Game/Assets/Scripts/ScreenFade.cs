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
		image = GetComponent<Image>();
	}

	void Update()
	{
		switch(state)
		{
		case FadeState.FadingToBlack:
			timer += Time.deltaTime;

			float alpha = 

			break;

		case FadeState.FadingToWhite:
			timer += Time.deltaTime;



			break;

		case FadeState.FadingToClear:
			timer += Time.deltaTime;
            
            
            
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