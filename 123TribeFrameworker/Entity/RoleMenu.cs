using System.ComponentModel.DataAnnotations;

namespace _123TribeFrameworker.Entity
{
    public class RoleMenu
    {
        [Key]
        public int id { get; set; }
        public string roleId { get; set; }
        public int  menuLevel { get; set; }
        public int menuId { get; set; }
    }

    public class RoleProp
    {
        public int menuId { get; set; }

        public string menuName { get; set; }
    }
}