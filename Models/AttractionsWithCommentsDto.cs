namespace Models;

public class AttractionWithCommentsDto
{
    public string Name { get; set; }           // rubrik/kategori
    public string Description { get; set; }    // beskrivning
    public List<string> Comments { get; set; } = new List<string>(); // bara kommentarerna
}