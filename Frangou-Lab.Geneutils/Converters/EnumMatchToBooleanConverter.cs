#region License

// Copyright 2018 Frangou Lab
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Globalization;
using WPFConverters;

namespace FrangouLab.Geneutils.Converters
{
    public class EnumMatchToBooleanConverter : BaseConverter
    {
        protected override object OnConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (IsNull(value, parameter))
                return false;

            var checkValue = value.ToString();
            var targetValue = parameter.ToString();

            return checkValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override object OnConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (IsNull(value, parameter))
                return null;

            var useValue = (bool) value;
            var targetValue = parameter.ToString();

            return useValue ? Enum.Parse(targetType, targetValue) : null;
        }

        private static bool IsNull(object value, object parameter)
        {
            return value == null || parameter == null;
        }
    }
}
