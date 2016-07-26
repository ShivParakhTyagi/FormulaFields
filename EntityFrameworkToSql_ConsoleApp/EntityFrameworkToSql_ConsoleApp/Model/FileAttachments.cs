namespace EntityFrameworkToSql_ConsoleApp.Model
{
    public class FileAttachments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public byte[] Content { get; set; }
    }
}