namespace Soot.Domain.Base
{
    public abstract class Root<T> where T : new()
    {
        public abstract T SetTrueId(object id);
    }
}
