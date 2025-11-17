using BlogApi.Models;
using BlogApi.Models.DtoS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewPost(AddPostDto addPostDto)
        {
            try
            {
                var post = new Posts 
                { 
                    Category = addPostDto.Category,
                    Post = addPostDto.Posts,
                    RegTime = DateTime.Now,
                    ModTime = DateTime.Now,
                    BloggerId   = addPostDto.BloggerId
                };

                using (var context = new BlogDbContext())
                {
                    if (post != null)
                    {
                        context.posts.Add(post);
                        context.SaveChanges();
                        return StatusCode(201, new { message = "Sikere hozzáadás", result = post });
                    }

                    return BadRequest(new { message = "Sikertelen hozzáadás", result = post });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Sikertelen hozzáadás", result = ex.Message });
            }
           
        }
    }
}
