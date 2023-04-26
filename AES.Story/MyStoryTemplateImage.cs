using System.Net;
using System.Net.Mime;

namespace AES.Story;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class MyStoryTemplateImage : MyStoryTemplateFileBased
{
    public static MyStoryTemplateImage CreateFromFile(string fileName, string title, Guid id = new(), int indexOrder = -1)
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
            //Bits = File.ReadAllBytes(fileName),
            FileName =  Path.GetFileName(fileName),
            ItemIndex = indexOrder
        };
    }

    public override StoryItem CreateStoryItem()
    {
        var newItem = new StoryImage
        {
            Template = this,
            DateCreated = DateTimeOffset.Now,
        };
        return newItem;
    }
}