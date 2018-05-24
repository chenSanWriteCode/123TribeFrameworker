using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _123TribeFrameworker.Models
{
    /// <summary>
    /// 编辑角色model
    /// </summary>
    public class RoleEditModel
    {
        /// <summary>
        /// 角色
        /// </summary>
        public AplicationRole role { get; set; }
        /// <summary>
        /// 角色包括人员
        /// </summary>
        public List<ApplicationUser> members { get; set; }
        /// <summary>
        /// 角色未包括人员
        /// </summary>
        public List<ApplicationUser> nonMembers { get; set; }
    }
    public class RoleMidifiedModel
    {
        public string  roleName { get; set; }
        /// <summary>
        /// 需要增加的人员ids
        /// </summary>
        public string[] addUsersIds { get; set; }
        /// <summary>
        /// 需要删除的ids
        /// </summary>
        public string[] deleteUsersIds { get; set; }
    }
    public class UserEdit
    {
        public string id { get; set; }
        [Required]
        public string userName { get; set; }
        public string trueName { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [Phone]
        public string phoneNumber { get; set; }
    }
    public class UpdatePasswordView
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "原密码")]
        public string beforPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        public string id { get; set; }
    }
    public class LoginViewModel
    {

        [Required]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public string id { get; set; }
        [Required]
        public string loginName { get; set; }
        public string userName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [Phone]
        public string phoneNumber { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "代码")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "记住此浏览器?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }
}
