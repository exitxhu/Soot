using System.Text.RegularExpressions;
using Soot.Domain.Base;
using Soot.Domain.Exceptions;

namespace Soot.Domain.ValueObjects
{
    public class EmailAddress : BaseVO
    {
        const string regex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";
        public string Address { get; }
        public EmailAddress(string address)
        {
            Address = Regex.IsMatch(address.Trim(), regex)
                ? address
                : throw new InvalidEmailAddressFormatException() ;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }
        public override string ToString() => Address;
    }
}