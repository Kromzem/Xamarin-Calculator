using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
	public static class Extension
	{
		public static T RemoveLast<T>(this List<T> list)
		{
			if (list.Count <= 0) return default;

			var element = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);

			return element;
		}
	}
}
