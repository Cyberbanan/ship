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

	private bool restartNext = false;
	private bool nextLevelNext = false;
	private bool creditsNext = false;
	private bool titleNext = false;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			image = GetComponent<Image>();

			SetColor(Color.black);
			FadeToClear();
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

				if(nextLevelNext)
				{
					nextLevelNext = false;
					Player.main.NextLevel();
					FadeToClear();
				}
				else if(restartNext)
				{
					restartNext = false;
					Player.main.Restart();
					FadeToClear();
				}
				else if(creditsNext)
				{
					creditsNext = false;
					Application.LoadLevel("DadHugCutscene");
					FadeToClear();
				}
				else if(titleNext)
				{
					titleNext = false;
					Application.LoadLevel("TitleScreen");
					FadeToClear();
				}
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

				if(nextLevelNext)
				{
					nextLevelNext = false;
					Player.main.NextLevel();
					FadeToClear();
				}
				else if(restartNext)
				{
					restartNext = false;
					Player.main.Restart();
					FadeToClear();
				}
				else if(creditsNext)
				{
					creditsNext = false;
					Application.LoadLevel("DadHugCutscene");
					FadeToClear();
				}
				else if(titleNext)
				{
					titleNext = false;
					Application.LoadLevel("TitleScreen");
					FadeToClear();
				}
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

				if(nextLevelNext)
				{
					nextLevelNext = false;
				}
				else if(restartNext)
				{
					restartNext = false;
				}
				else if(creditsNext)
				{
					creditsNext = false;
				}
				else if(titleNext)
				{
					titleNext = false;
				}
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
		timer = 0.0f;
		state = FadeState.FadingToClear;
		prevColor = new Color(image.color.r, image.color.g, image.color.b, image.color.a);
		targetColor = new Color(prevColor.r, prevColor.g, prevColor.b, 0.0f);
	}

	public void SetColor(Color c)
	{
		image.color = c;
	}

	public void RestartNext()
	{
		restartNext = true;
		nextLevelNext = false;
		creditsNext = false;
		titleNext = false;
	}

	public void NextLevelNext()
	{
		restartNext = false;
		nextLevelNext = true;
		creditsNext = false;
		titleNext = false;
	}

	public void CreditsNext()
	{
		restartNext = false;
		nextLevelNext = false;
		creditsNext = true;
		titleNext = false;
	}

	public void TitleNext()
	{
		restartNext = false;
		nextLevelNext = false;
		creditsNext = false;
		titleNext = true;
	}
}