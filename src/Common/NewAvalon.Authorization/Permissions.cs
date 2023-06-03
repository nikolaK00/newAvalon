using System.ComponentModel.DataAnnotations;

namespace NewAvalon.Authorization
{
    public enum Permissions
    {
        /// <summary>
        /// Read user.
        /// </summary>
        [Display(GroupName = "User", Name = "Read", Description = "Can read users")]
        UserRead = 1,


        /// <summary>
        /// Read order.
        /// </summary>
        [Display(GroupName = "Order", Name = "Read", Description = "Can read orders")]
        OrderRead = 100,

        /// <summary>
        /// Read order.
        /// </summary>
        [Display(GroupName = "Order", Name = "Create", Description = "Can create orders")]
        OrderCreate = 101,
    }
}
