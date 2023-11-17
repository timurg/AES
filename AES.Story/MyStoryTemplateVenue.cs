namespace AES.Story;

public class MyStoryTemplateVenue : MyStoryTemplateItem
{

    public static MyStoryTemplateVenue Create(string adress, string title, float latitude, float longitude, int indexOrder = -1, Guid id = new())
    {
        return new MyStoryTemplateVenue() { Id = id, Adress = adress, Title = title, Latitude = latitude, Longitude = longitude, ItemIndex = indexOrder, Description = adress};
    }

    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public string Adress { get; set; } = default!;

    /*
    public static MyStoryTemplateVenue Create(string , string[] options, int correctOptions,
        int indexOrder = -1, Guid id = new())
    {
       // return new
    }
    */
    public override StoryItem CreateStoryItem()
    {
        return new StoryVenue()
        {
            Adress = Adress,
            Latitude = Latitude,
            Longitude = Longitude,
            Title = Title,
            Template = this
        };
    }
    
    
}