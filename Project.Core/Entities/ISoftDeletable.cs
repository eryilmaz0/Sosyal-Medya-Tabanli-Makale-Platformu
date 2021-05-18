namespace Project.Core.Entities
{
    public interface ISoftDeletable
    {
         bool IsDeleted { get; set; }
    }
}