namespace AES.Story;

public class StoryVenue: StoryItem
{
    public float Longitude { get; set; }
    public float Latitude { get; set; }

    public string Title { get; set; } = default!;
    public string Adress { get; set; } = default!;
}