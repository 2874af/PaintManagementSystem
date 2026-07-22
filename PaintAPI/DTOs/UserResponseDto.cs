using System;

namespace PaintAPI.DTOs;

public class UserResponseDto
{
    public DateTime CreatedDate { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
