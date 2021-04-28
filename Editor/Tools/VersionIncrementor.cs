using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace SSR.Editor
{
	[InitializeOnLoad]
	public class VersionIncrementor
	{
		[PostProcessBuild(1)]
		public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
		{
			Debug.Log("[Tools] Build v" + PlayerSettings.bundleVersion + " (" + PlayerSettings.Android.bundleVersionCode + ")");
			IncreaseBuild();
		}

		static void IncrementVersion(int majorIncr, int minorIncr, int buildIncr)
		{
			string[] lines = PlayerSettings.bundleVersion.Split('.');

			int MajorVersion = int.Parse(lines[0]) + majorIncr;
			int MinorVersion = int.Parse(lines[1]) + minorIncr;
			int Build = 0;
			if (lines.Length > 2)
			{
				Build = int.Parse(lines[2]) + buildIncr;
			}

			PlayerSettings.bundleVersion = MajorVersion.ToString("0") + "." + MinorVersion.ToString("0") + "." + Build.ToString("0");
			PlayerSettings.Android.bundleVersionCode = MajorVersion * 10000 + MinorVersion * 1000 + Build;
			PlayerSettings.iOS.buildNumber = PlayerSettings.bundleVersion;
			Debug.Log("[Tools] Version changed to " + PlayerSettings.bundleVersion + " (android=" + PlayerSettings.Android.bundleVersionCode + ", ios=" + PlayerSettings.iOS.buildNumber + ")");
		}

		[MenuItem("Tools/SSR/Version/Increase Minor")]
		private static void IncreaseMinor()
		{
			IncrementVersion(0, 1, 0);
		}

		[MenuItem("Tools/SSR/Version/Decrease Minor")]
		private static void DecreaseMinor()
		{
			IncrementVersion(0, -1, 0);
		}

		[MenuItem("Tools/SSR/Version/Increase Major")]
		private static void IncreaseMajor()
		{
			IncrementVersion(1, 0, 0);
		}

		[MenuItem("Tools/SSR/Version/Decrease Major")]
		private static void DecreaseMajor()
		{
			IncrementVersion(-1, 0, 0);
		}

		private static void IncreaseBuild()
		{
			IncrementVersion(0, 0, 1);
		}
	}
}
