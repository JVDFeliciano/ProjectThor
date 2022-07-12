public class Product
{
    public int Id { get; set; }
    public String Code { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Tag> Tags { get; set; }
}
