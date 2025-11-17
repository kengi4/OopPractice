namespace OopPractice.Data
{
    public abstract class SaveBase
    {
        public string? SaveName { get; set; }
        public DateTime LastModified { get; set; } = DateTime.Now;
    }
}