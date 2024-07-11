using baskidabaski.Areas.Admin.Models;
using baskidabaski.Identity;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager,UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values=_roleManager.Roles.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                var role1 = new AppRole() { Name=role.Name};
                var result = await _roleManager.CreateAsync(role1);
                if (result.Succeeded)
                {
                    return  RedirectToAction("Index", "Role", new { area = "Admin" });
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
         
            return View(role);
        }

        [HttpGet]

        public async Task<IActionResult> roleupdate(int Id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x=>x.Id==Id);
            if (role != null)
            {
                var values=new RoleEditModel() { RoleId=role.Id,RoleName=role.Name}; 
                return View(values);
            }


            return RedirectToAction("Index", "Role", new { area = "Admin" });

        }

        [HttpPost]
        public async Task<IActionResult> roleupdate(RoleEditModel model)
        {
            if (ModelState.IsValid)
            {
                var role =  _roleManager.Roles.FirstOrDefault(x=>x.Id==model.RoleId);
                role.Name=model.RoleName;
                var sonuc=await _roleManager.UpdateAsync(role);
                if (sonuc.Succeeded)
                { return RedirectToAction("Index", "Role", new { area = "Admin" });
                }

                return View(model);

            }
            return View(model);

        }
        public async Task<IActionResult> UserListByRoleId(int Id) {
           
            var role=_roleManager.Roles.FirstOrDefault(x=>x.Id==Id); 
            if (role!=null)
            {
               var users =await _userManager.GetUsersInRoleAsync(role.Name);
            return View(users);

            }
            return RedirectToAction("Index", "Role", new { area = "Admin" });

        }



        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRoles(int Id) { 
        var user=_userManager.Users.FirstOrDefault(x=>x.Id==Id);
            var roles = _roleManager.Roles.ToList();

            var userroles =await _userManager.GetRolesAsync(user);
            var rolelist=new List<RoleAssingViewModel>();
            foreach (var item in roles)
            {
                var role = new RoleAssingViewModel() { RoleId = item.Id, RoleName = item.Name, RoleExist = userroles.Contains(item.Name) };
                rolelist.Add(role); 
            }
            return View(new AssignRoleModel() { UserId=Id,UserName=user.UserName,RoleAssingViewModels=rolelist});

        }

        [HttpPost]
        public async Task<IActionResult> AssignRoles(AssignRoleModel model)
        {
           var user= _userManager.Users.FirstOrDefault(x => x.Id == model.UserId);
            foreach (var role in model.RoleAssingViewModels)
            {
                if (role.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
            }


            return RedirectToAction("UserList", "Role", new { area = "Admin" });
        }
    }
}
