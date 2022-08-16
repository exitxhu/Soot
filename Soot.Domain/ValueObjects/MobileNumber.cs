using System.Text.RegularExpressions;
using Soot.Domain.Base;
using Soot.Domain.Exceptions;

namespace Soot.Domain.ValueObjects
{
    public class MobileNumber : BaseVO
    {
        public string Number { get; }
        public MobileNumber(string number)
        {
            Number =  Regex.IsMatch(number, @"\+?\d{9,15}")
                ? number
                : throw new InvalidMobileNumberFormatException();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
        public override string ToString() => Number;
    }
}