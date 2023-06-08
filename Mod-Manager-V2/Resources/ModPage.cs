namespace Mod_Manager_V2.Resources
{
    public class ModPage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public string FileLink { get; set; }
        public string UploadDate { get; set; }
        public string Category { get; set; }
        public string[] FilePaths { get; set; }
    }
}