using System;
using System.Collections.Generic;
using System.Linq;

namespace SocNetMockup.DtoMappers
{
    public class Mapper<TFrom, TTo>
    {
        private readonly Func<TFrom, TTo> map;

        public Mapper(Func<TFrom, TTo> map)
        {
            this.map = map;
        }

        public TTo Map(TFrom from) => map(from);
        public IEnumerable<TTo> Map(IEnumerable<TFrom> from) => from.Select(x => map(x));
    }
}
