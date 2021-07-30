using System;
using System.Collections.Generic;
using System.Linq;

namespace SocNetMockup.DtoMappers
{
    public class Mapper<TFrom, TTo> where TFrom : class where TTo : class
    {
        private readonly Func<TFrom, TTo> map;

        public Mapper(Func<TFrom, TTo> map)
        {
            this.map = map;
        }

        public TTo Map(TFrom from)
        {
            if (from == null) return null;
            return map(from);
        }

        public IEnumerable<TTo> Map(IEnumerable<TFrom> from) => from.Select(Map);
    }
}
