namespace Soot.Domain.Base
{
    public abstract class BaseVo
    {
        protected static bool EqualOperator(BaseVo left, BaseVo? right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(BaseVo left, BaseVo? right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            var other = (BaseVo)obj;
            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x.GetHashCode())
                .Aggregate((x, y) => x ^ y);
        }
        public static bool operator ==(BaseVo left, BaseVo? right) => EqualOperator(left, right);
        public static bool operator !=(BaseVo left, BaseVo? right) => !EqualOperator(left, right);
    }
}
