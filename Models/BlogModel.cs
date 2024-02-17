// Imports the namespace for moment2
using System.ComponentModel.DataAnnotations;
namespace moment2.Models;

// Defines the class BlogModel
public class BlogModel
{

    //Declares the property for Title with get and set
    //Required is used to prevent the form from being submitted if a title has not been set
    //An error message is displayed if the form is submitted without a title
    [Required(ErrorMessage = "Please add a title to you blog post")]
    public string? Title { get; set; }

    //Declares the property for Post with get and set
    //Required is used to prevent the form from being submitted if a post has not been set
    //MinLength(100) is used to prevent the form from being submitted if the post contains less than 100 characters
    //An error message is displayed if the form is submitted without a post or if the post is shorter than 100 characters
    [Required(ErrorMessage = "Please add a blog post")]
    [MinLength(100, ErrorMessage = "The blog post must be at least 100 characters long")]
    public string? Post { get; set; }

    //Declares the property for Author with get and set
    //Required is used to prevent the form from being submitted if an author has not been set
    //An error message is displayed if the form is submitted without an author
    [Required(ErrorMessage = "Please add your name")]
    public string? Author { get; set; }

    //Declares the property for Id with get and set
    public int Id { get; set; }

    //A static counter that add new Ids to blog posts
    private static int _idCounter = 1;
    //To ensure unique Ids the counter takes the Id created for the previous post and increments this by 1
    public BlogModel()
    {
        Id = _idCounter++;
    }

    //Declares the property for Date with get and set
    // DateTime.Now is UnobservedTaskExceptionEventArgs to set the current date and time
    public DateTime Date { get; set; } = DateTime.Now;

}
