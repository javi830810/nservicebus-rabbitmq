﻿using System;
using System.Collections.Generic;

namespace ScaleBridge.Message
{
	public static class Helpers{
		public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
		{
			if (second == null || first == null) return;
			foreach (var item in second) 
				if (!first.ContainsKey(item.Key)) 
					first.Add(item.Key, item.Value);
		}
	}
}

