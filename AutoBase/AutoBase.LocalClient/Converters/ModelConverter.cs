using AutoBase.Common;
using AutoBase.DAL.AutoBaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace AutoBase.LocalClient.Converters
{
    public class ModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string retval = Constants.Empty; ;
            if (value is IEnumerable<Model>)
            {
                IEnumerable<string> namesList = (value as IEnumerable<Model>).Select(x => x.ModelName);
                retval = string.Join(", ", namesList);
            }
            return retval;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
