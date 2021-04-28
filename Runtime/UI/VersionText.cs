using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SSR.UI
{
	public class VersionText : MonoBehaviour
	{
		[SerializeField] private Text label = null;
		[SerializeField] private string display = "ver.{0}";

		private void Reset()
		{
			if (label == null)
				label = GetComponent<Text>();
		}

		private void Start()
		{
			if (label != null)
				label.text = string.Format(display, Application.version);
		}

	}
}