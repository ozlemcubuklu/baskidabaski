using Entity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace baskidabaski.Areas.Admin.Models
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class RoleEditModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class RoleAssingViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleExist { get; set; }
    }
    public class AssignRoleModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleAssingViewModel> RoleAssingViewModels { get; set; }
    }
}
