using MyWebApplication.Models.DB;
using MyWebApplication.Models.ViewModel;

namespace MyWebApplication.Models.EntityManager
{
    public class UserManager
    {
        public void AddUserAccount(UserModel user)
        {
            using (MyDBContext db = new MyDBContext())
            {
                //Add checking here if login exists

                SystemUsers newSysUser = new SystemUsers
                {
                    LoginName = user.LoginName,
                    PasswordEncryptedText = user.Password, //this has to be encrypted
                    CreatedDateTime = DateTime.Now,
                    ModifiedDateTime = DateTime.Now
                };

                db.SystemUsers.Add(newSysUser);
                db.SaveChanges();

                int newUserId = db.SystemUsers
                    .First(u => u.LoginName == newSysUser.LoginName)
                    .UserID;

                Users newUser = new Users
                {
                    ProfileID = 0,
                    UserID = newUserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    CreatedBy = newUserId,
                    CreatedDateTime = DateTime.Now,
                    ModifiedBy = 1,
                    ModifiedDateTime = DateTime.Now,
                    AccountImage = user.AccountImage
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                int roleId = db.Role.First(r => r.RoleName == "Member").RoleID;

                UserRole userRole = new UserRole
                {
                    UserID = newUserId,
                    LookUpRoleID = roleId,
                    IsActive = true,
                    CreatedBy = newUserId,
                    CreatedDateTime = DateTime.Now,
                    ModifiedBy = newUserId,
                    ModifiedDateTime = DateTime.Now,
                };

                db.UserRole.Add(userRole);
                db.SaveChanges();
            }
        }

        public void UpdateUserAccount(UserModel user)
        {
            UpdateUserAccount(user, user.LoginName);
        }

        public void UpdateUserAccount(UserModel user, string username)
        {
            using (MyDBContext db = new MyDBContext())
            {
                // Check if a user with the given login name already exists
                SystemUsers existingSysUser = db.SystemUsers.FirstOrDefault(
                    u => u.LoginName == username
                );
                Users existingUser = db.Users.FirstOrDefault(
                    u => u.UserID == existingSysUser.UserID
                );

                if (existingSysUser == null && existingUser == null)
                {
                    return;
                }

                // Update the existing user
                existingSysUser.ModifiedBy = 1; // This has to be updated
                existingSysUser.ModifiedDateTime = DateTime.Now;
                existingSysUser.LoginName = user.LoginName;
                existingSysUser.PasswordEncryptedText = user.Password;

                // You can also update other properties of the user as needed
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Gender = user.Gender;

                db.SaveChanges();
            }
        }

        public UsersModel GetAllUsers()
        {
            UsersModel list = new UsersModel();

            using (MyDBContext db = new MyDBContext())
            {
                var users =
                    from u in db.Users
                    join us in db.SystemUsers on u.UserID equals us.UserID
                    join ur in db.UserRole on u.UserID equals ur.UserID
                    join r in db.Role on ur.LookUpRoleID equals r.RoleID
                    select new
                    {
                        u,
                        us,
                        r,
                        ur
                    };

                list.Users = users
                    .Select(
                        records =>
                            new UserModel()
                            {
                                LoginName = records.us.LoginName,
                                FirstName = records.u.FirstName,
                                LastName = records.u.LastName,
                                Gender = records.u.Gender,
                                CreatedBy = records.u.CreatedBy,
                                AccountImage = records.u.AccountImage ?? string.Empty,
                                RoleID = records.ur.LookUpRoleID,
                                RoleName = records.r.RoleName
                            }
                    )
                    .ToList();
            }

            return list;
        }

        public UserModel GetUser(string user)
        {
            UserModel userModel = new();

            using (MyDBContext db = new MyDBContext())
            {
                var users =
                    from u in db.Users
                    join us in db.SystemUsers on u.UserID equals us.UserID
                    join ur in db.UserRole on u.UserID equals ur.UserID
                    join r in db.Role on ur.LookUpRoleID equals r.RoleID
                    where us.LoginName == user
                    select new
                    {
                        u,
                        us,
                        r,
                        ur
                    };

                userModel = users
                    .Select(
                        records =>
                            new UserModel()
                            {
                                LoginName = records.us.LoginName,
                                FirstName = records.u.FirstName,
                                LastName = records.u.LastName,
                                Gender = records.u.Gender,
                                CreatedBy = records.u.CreatedBy,
                                AccountImage = records.u.AccountImage ?? string.Empty,
                                RoleID = records.ur.LookUpRoleID,
                                RoleName = records.r.RoleName
                            }
                    )
                    .First();
            }

            return userModel;
        }

        public bool IsLoginNameExist(string loginName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                return db.SystemUsers.Where(u => u.LoginName.Equals(loginName)).Any();
            }
        }

        public string GetUserPassword(string loginName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                var user = db.SystemUsers.Where(o => o.LoginName.ToLower().Equals(loginName));
                if (user.Any())
                    return user.FirstOrDefault().PasswordEncryptedText;
                else
                    return string.Empty;
            }
        }

        public bool IsUserInRole(string loginName, string roleName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                SystemUsers su = db.SystemUsers
                    .Where(o => o.LoginName.ToLower().Equals(loginName))
                    ?.FirstOrDefault();

                if (su != null)
                {
                    var roles =
                        from ur in db.UserRole
                        join r in db.Role on ur.LookUpRoleID equals r.RoleID
                        where r.RoleName.Equals(roleName) && ur.UserID.Equals(su.UserID)
                        select r.RoleName;
                    if (roles != null)
                    {
                        return roles.Any();
                    }
                }
                return false;
            }
        }
    }
}