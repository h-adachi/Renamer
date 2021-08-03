using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renamer
{
	class Util
	{
		public enum eItemType
		{
			ItemType_File,
			ItemType_Dir,
			ItemType_ALL,
		}

		public enum eSelectedType
		{
			Selected_All,
			Selected_Any,
		}

		public static String CleateName(String name, int startIndex, int endIndex, int middleIndex, int middleLength)
		{
			if (String.IsNullOrEmpty(name)) return "";

			String extension = System.IO.Path.GetExtension(name);
			name = System.IO.Path.GetFileNameWithoutExtension(name);
			if (String.IsNullOrEmpty(name)) return extension;

			if (name.Length <= startIndex) return "";
			name = name.Substring(startIndex);
			if (name.Length <= endIndex) return "";
			name = name.Substring(0, name.Length - endIndex);
			if (middleLength != 0)
			{
				if (name.Length <= middleLength + middleIndex) return "";
				name = name.Substring(0, middleIndex) + name.Substring(middleIndex + middleLength);
			}
			return name + extension;
		}
	}
}
