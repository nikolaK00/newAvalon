using System.ComponentModel.DataAnnotations;

namespace NewAvalon.Authorization
{
    public enum Permissions
    {
        /// <summary>
        /// Read dealerships.
        /// </summary>
        [Display(GroupName = "User", Name = "Read", Description = "Can read users")]
        UserRead = 1,
    }
}
