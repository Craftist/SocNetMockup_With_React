using System.ComponentModel.DataAnnotations;

namespace SocNetMockup.Util.Attributes
{
    public class StringLengthRangeAttribute : StringLengthAttribute
    {
        public StringLengthRangeAttribute(int min, int max) : base(max)
        {
            MinimumLength = min;
        }
    }
}
