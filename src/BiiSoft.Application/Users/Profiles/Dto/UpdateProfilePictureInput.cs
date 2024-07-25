using System;
using System.ComponentModel.DataAnnotations;

namespace BiiSoft.Users.Profiles.Dto
{
    public class UpdateProfilePictureInput
    {
        [Required]
        public Guid ProfilePictureId { get; set; }
    }
}