using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.WebApi.Models.Forms.Comment;

#nullable disable
public class CommentForm
{
    [Required(ErrorMessage = "Content required."), MinLength(2)]
    public string Content { get; init; }
}
