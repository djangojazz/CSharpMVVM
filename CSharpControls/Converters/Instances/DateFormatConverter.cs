using System;                  
using System.Globalization; 
using System.Windows.Controls;
using System.Windows.Data;

namespace CSharpControls.Converters.Instances
{
  public sealed class DateFormatConverter : Control, IValueConverter
  {
    public string DateFormat { get; set; } = "MM/dd/yyyy";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var dt = new DateTime((long)(double)value);

      return dt.ToString(DateFormat);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
