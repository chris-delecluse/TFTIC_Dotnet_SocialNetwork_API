using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebApi.Models.Forms.Post;

#nullable disable
public class PostForm
{
    [Required(ErrorMessage = "Content required."), MinLength(2)]
    public string Content { get; init; }
}
