using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSR.UI
{
	public class FPSText : MonoBehaviour
	{
		[SerializeField] private float updatePeriod = 0.5f;
		[SerializeField] private string display = "{0} FPS";
		[SerializeField] private Text label = null;

		private int fpsAccumulator = 0;
		private float updateTime = 0;
		private int currentFps;

		private void Reset()
		{
			if (label == null)
				label = GetComponent<Text>();
		}

		private void Update()
		{
			fpsAccumulator++;
			var time = Time.realtimeSinceStartup;
			if (time < updateTime || label == null)
				return;

			updateTime = time + updatePeriod;
			currentFps = (int)(fpsAccumulator / updatePeriod);
			fpsAccumulator = 0;
			label.text = string.Format(display, currentFps);
		}
	}
}