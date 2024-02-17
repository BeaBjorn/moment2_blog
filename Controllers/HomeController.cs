//Including namespaces for System.Text.Json and Microsoft.AspNetCore.Mvc
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
//Links the controller to the Model
using moment2.Models;

//namespace for HomeController
namespace moment2.Controllers
{
    // Defines the class HomeController
    public class HomeController : Controller
    {
        //Stayic counter to provide unique Ids for blog posts
        private static int _idCounter = 1;

        //Declares the BlogModel list
        private List<BlogModel> blog;

        //HomeController constructor
        //Uses ReadAllText to get all objects from the json file and stores this in the variable jsonstr
        //The objects returned form the json file is deserialized and stored in the variable "blog"
        public HomeController()
        {
            string jsonStr = System.IO.File.ReadAllText("/Users/beatricebjorn/Desktop/blog.json");
            blog = JsonSerializer.Deserialize<List<BlogModel>>(jsonStr) ?? new List<BlogModel>();
        }

        //Route for the home view displaying all blog posts
        //Orders the blog posts in descending order according to the date and time the posts were created
        //ViewData is used to store the number of blog posts available
        //Two instances of ViewBag os used to store success messages for when a post is created and when a post is deleted
        //Returns the index page with all blog posts
        [Route("all-blog-posts")]
        public IActionResult Index()
        {
            var allPosts = blog.OrderByDescending(b => b.Date);
            ViewData["CountPosts"] = blog?.Count ?? 0;
            ViewBag.Success = TempData["Success"];
            ViewData["Delete"] = TempData["Delete"];
            return View(allPosts.ToList());
        }

        //Route for the about view
        //Returns the view, containing a blog posts
        [Route("/real-blog-post")]
        public IActionResult About()
        {
            return View();
        }

        //Route for the AddBlogPost view
        //ViewData holds the number of blog posts created
        //Returns the AddBlogPosts view
        [Route("/add-post")]
        public IActionResult AddBlogPost()
        {
            ViewData["CountPosts"] = blog?.Count ?? 0;
            return View();
        }

        //Route for the AddBlogPosts view
        //Http POST request to add new blog post
        [Route("/add-post")]
        [HttpPost]
        public IActionResult AddBlogPost(BlogModel model)
        {
            //If-statement checks if ModelState is valid according to the requirements in the model
            //If the data entered is valid the current date and time and a unique Id is added to the blog list
            //The Json file is updated with the new data
            //The form is cleared of values, a success message is stored in TempData and the user gets redirected to the home page
            if (ModelState.IsValid)
            {
                model.Date = DateTime.Now;
                model.Id = _idCounter++;

                blog.Add(model);

                string jsonStr = JsonSerializer.Serialize(blog);
                System.IO.File.WriteAllText("/Users/beatricebjorn/Desktop/blog.json", jsonStr);

                ModelState.Clear();

                TempData["Success"] = "Blog post added successfully!";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //Http POST request to delete blog post
        [HttpPost]
        public IActionResult DeleteBlogPost(int id)
        {
            //Finds the post with corresponding Id and stores this in the variable "deletePost"
            //An if-statement checks if the variable is not null
            //If so, Remove is used to delete the post
            //The Json file is updated with the remaining blog posts after a removal
            //TempData stores a message about a blog post being deleted successfully
            //Return redirects to homepage with the updated list of blog posts
            var deletePost = blog.FirstOrDefault(blog => blog.Id == id);

            if (deletePost != null)
            {
                blog.Remove(deletePost);

                string updatedJson = JsonSerializer.Serialize(blog);
                System.IO.File.WriteAllText("/Users/beatricebjorn/Desktop/blog.json", updatedJson);
            }
            TempData["Delete"] = "The blog post has been deleted!";
            return RedirectToAction("Index", "Home");
        }

    }
}
