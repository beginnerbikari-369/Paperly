namespace DocsCloneApi.Models
{
    public class Document
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
    }
}
