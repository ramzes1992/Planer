using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using Model.Enums;
using System.Windows.Media;

namespace Planer.Converters
{
    public class TaskSateToColorConverter : IValueConverter
    {
        private readonly Color NotStartedColor = Colors.Black;
        private readonly Color WorkInProgresColor = Colors.DarkOrange;
        private readonly Color DoneColor = Colors.LightGray;


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var state = (TaskState)value;

            if(state != null)
            {
                switch(state)
                {
                    case TaskState.NotStarted:
                        return new SolidColorBrush(NotStartedColor);
                    case TaskState.WorkInProgres:
                        return new SolidColorBrush(WorkInProgresColor);
                    case TaskState.Done:
                        return new SolidColorBrush(DoneColor);
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
