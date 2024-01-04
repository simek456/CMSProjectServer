using System.ComponentModel.DataAnnotations;

namespace CMSProjectServer.Domain.Dto;

public class UserLoginDto
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}