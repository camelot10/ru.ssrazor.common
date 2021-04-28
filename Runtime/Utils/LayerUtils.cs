using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSR.Utils
{
	public static class LayerUtils
	{
		public static LayerMask Default()
		{
			return NamesToMask("Default");
		}

		public static LayerMask Create(Component comp)
		{
			return Create(comp.gameObject);
		}

		public static LayerMask Create(GameObject gameObject)
		{
			var mask = new LayerMask()
			{
				value = (1 << gameObject.layer),
			};
			return mask;
		}

		public static LayerMask Create(params string[] layerNames)
		{
			return NamesToMask(layerNames);
		}

		public static LayerMask Create(params int[] layerNumbers)
		{
			return LayerNumbersToMask(layerNumbers);
		}

		public static LayerMask NamesToMask(params string[] layerNames)
		{
			LayerMask ret = (LayerMask)0;
			foreach (var name in layerNames)
			{
				ret |= (1 << LayerMask.NameToLayer(name));
			}
			return ret;
		}

		public static LayerMask LayerNumbersToMask(params int[] layerNumbers)
		{
			LayerMask ret = (LayerMask)0;
			foreach (var layer in layerNumbers)
			{
				ret |= (1 << layer);
			}
			return ret;
		}

		public static LayerMask Inverse(this LayerMask original)
		{
			return ~original;
		}

		public static LayerMask AddToMask(this LayerMask original, LayerMask extra)
		{
			return original | extra;
		}

		public static LayerMask AddToMask(this LayerMask original, params string[] layerNames)
		{
			return AddToMask(original, NamesToMask(layerNames));
		}

		public static LayerMask RemoveFromMask(this LayerMask original, LayerMask extra)
		{
			LayerMask invertedOriginal = ~original;
			return ~(invertedOriginal | extra);
		}

		public static LayerMask RemoveFromMask(this LayerMask original, params string[] layerNames)
		{
			return RemoveFromMask(original, NamesToMask(layerNames));
		}

		public static string[] MaskToNames(this LayerMask original)
		{
			var output = new List<string>();

			for (int i = 0; i < 32; ++i)
			{
				int shifted = 1 << i;
				if ((original & shifted) == shifted)
				{
					string layerName = LayerMask.LayerToName(i);
					if (!string.IsNullOrEmpty(layerName))
					{
						output.Add(layerName);
					}
				}
			}
			return output.ToArray();
		}

		public static string MaskToString(this LayerMask original)
		{
			return MaskToString(original, ", ");
		}

		public static string MaskToString(this LayerMask original, string delimiter)
		{
			return string.Join(delimiter, MaskToNames(original));
		}
	} 
}