using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SocNetMockup.Data.ValueConverters
{
    public class GuidListToStringValueConverter : ValueConverter<IEnumerable<Guid>, string>
    {
        public GuidListToStringValueConverter()
            : base(guids => ListToString(guids), str => StringToList(str)) { }

        public static string ListToString(IEnumerable<Guid> guids)
        {
            var guidArray = guids is null ? null : guids as Guid[] ?? guids.ToArray();
            if (guidArray is null || !guidArray.Any()) {
                return string.Empty;
            }

            return string.Join(",", guidArray);
        }

        public static IEnumerable<Guid> StringToList(string str)
        {
            if (str is null || str.Length == 0) {
                return Enumerable.Empty<Guid>();
            }

            return str.Split(",").Select(Guid.Parse);
        }
    }
}
