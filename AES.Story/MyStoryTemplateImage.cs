using System.Net;
using System.Net.Mime;

namespace AES.Story;

public class MyStoryTemplateImage : MyStoryTemplateItem
{
    public byte[] Bits { get; set; }
    public string ContentType { get; set; }
    
    public string FileName { get; set; }

    public static MyStoryTemplateImage CreateFromFile(string fileName, string title, Guid id = new())
    {
        var types = new Dictionary<string, string>()
        {
            { ".bmp", "image/bmp" },
            { ".gif", "image/gif" },
            { ".ico", "image/x-icon" },
            { ".jpeg", "image/jpeg" },
            { ".jpg", "image/jpeg" },
            { ".png", "image/png" },
            { ".svg", "image/svg+xml" },
            { ".tif", "image/tiff" },
            { ".tiff", "image/tiff" },
            { ".webp", "image/webp" }
        };

        var ext = Path.GetExtension(fileName).ToLower();
        return new MyStoryTemplateImage()
        {
            Id = id,
            Title = title,
            Description = "",
            ContentType = types.ContainsKey(ext) ? types[ext] : "application/octet-stream",
            Bits = File.ReadAllBytes(fileName),
            FileName =  Path.GetFileName(fileName)
        };
    }
}