using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Renamer
{
	class NameConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			String name = (String)values[0];
			int startIndex = (int)values[1];
			int endIndex = (int)values[2];
			int middleIndex = (int)values[3];
			int middleLength = (int)values[4];

			return Util.CleateName(name, startIndex, endIndex, middleIndex, middleLength);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
