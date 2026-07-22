using System;
using System.ComponentModel.DataAnnotations;

namespace PaintAPI.DTOs;

public class CreateUserDto
{
    [Required(ErrorMessage = "Name can not be empty.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email can not be empty.")]
    [EmailAddress(ErrorMessage = "Email is not apropriate.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessageResourceName = "Phone can not be empty.")]
    public string Phone { get; set; } = string.Empty;
}
