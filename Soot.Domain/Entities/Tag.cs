using Soot.Domain.Base;

namespace Soot.Domain.Entities
{
    public class Tag : Root<Tag>
    {
        public static Tag RawInstance => new Tag();
        public Tag()
        {

        }
        public int TagId { get; set; }
        public string TagName { get; set; }

        public override Tag SetTrueId(object id)
        {
            TagId = (int)id;
            return this;
        }
    }
}
