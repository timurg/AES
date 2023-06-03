namespace AES.Story;

public class MyStoryTemplateVenue : MyStoryTemplateItem
{
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    
    public string Title { get; set; }
    public string Adress { get; set; }

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
            Title = Title
        };
    }
    
    
}