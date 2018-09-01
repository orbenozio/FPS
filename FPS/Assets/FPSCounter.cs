using UnityEngine;

public class FPSCounter : MonoBehaviour 
{
	public int frameRange = 60;
	public int AverageFPS { get; private set; }

	public int HighestFPS { get; private set; }
	public int LowestFPS { get; private set; }
	
	int[] fpsBuffer;
	int fpsBufferIndex;
	
	private void Update()
	{
		if (fpsBuffer == null || fpsBuffer.Length != frameRange) 
		{
			InitializeBuffer();
		}
		
		UpdateBuffer();
		CalculateFPS();
	}
	
	private void InitializeBuffer () 
	{
		if (frameRange <= 0) 
		{
			frameRange = 1;
		}
	
		fpsBuffer = new int[frameRange];
		fpsBufferIndex = 0;
	}
	
	private void UpdateBuffer () 
	{
		fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
		
		if (fpsBufferIndex >= frameRange) 
		{
			fpsBufferIndex = 0;
		}
	}
	
	private void CalculateFPS () 
	{
		var sum = 0;
		var highest = 0;
		var lowest = int.MaxValue;
		
		for (var i = 0; i < frameRange; i++) 
		{
			var fps = fpsBuffer[i];
			sum += fps;
			
			if (fps > highest) 
			{
				highest = fps;
			}
			
			if (fps < lowest) 
			{
				lowest = fps;
			}
		}
		
		AverageFPS = sum / frameRange;
		HighestFPS = highest;
		LowestFPS = lowest;
	}
}