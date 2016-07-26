using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkToSql_ConsoleApp.Model;
using EntityFrameworkToSql_ConsoleApp.Services;

namespace EntityFrameworkToSql_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EntityModel x = new EntityModel();
            //x.runDb();
            //x.runDb_roleAssignment();
            SqlModel y = new SqlModel();
            y.runDb().Wait();
        }
    }

    class EntityModel
    {
        public void runDb()
        {
            try
            {
                var go=Console.ReadLine();
                if (go!=null&&go.Contains("N"))
                {
                    return;
                }
                using (var db = new MobilizeDbSchema())
                {
                    var R = db.Users?.ToList();
                    if (R == null)
                    {
                        return;
                    }
                    var user = new User()
                    {
                        UserLogin = new Login()
                        {
                            LoginId = "abc",
                            Password = "abc",
                        },
                        Name = "abc",
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    Console.WriteLine(user.UserId);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void runDb_roleAssignment()
        {
            try
            {

                using (var db = new MobilizeDbSchema())
                {
                    var res = (from u in db.Users where u.UserId == 1 select u).FirstOrDefault();

                    var user = res;
                    var role = new Role()
                    {
                        RoleName = "role_abc",
                        OrganizationId = 1,
                        RoleDescription = "desc",
                        RoleOrganization = res.UserOrganization,
                    };
                    db.Roles.Add(role);

                    var role2 = new Role()
                    {
                        RoleName = "rolabce_abc",
                        OrganizationId = 1,
                        RoleDescription = "desc",
                        RoleOrganization = res.UserOrganization,
                    };
                    db.Roles.Add(role);

                    res.UserRoles.Add(role);
                    res.UserRoles.Add(role2);
                    db.SaveChanges();
                    Console.WriteLine(user.UserId);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    class SqlModel
    {
        private string connectionString =
            "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\IMentor\\Source\\Repos\\FormulaFields\\EntityFrameworkToSql_ConsoleApp\\EntityFrameworkToSql_ConsoleApp\\Database\\localDb.mdf;";

        public async Task runDb()
        {
            try
            {
                var go = Console.ReadLine();
                if (go != null && go.Contains("N"))
                {
                    return;
                }
                SqlService service = new SqlService();

                var user = new User()
                {
                    UserLogin = new Login()
                    {
                        LoginId = "abc",
                        Password = "abc",
                    },
                    Name = "abc",
                };

                Console.WriteLine(user.Name);
                var sqlStatement =
                    $"SELECT U.*, R.*, UR.*, L.*, O.* " +
                    $"FROM Users " + 
                    $"   AS U" +
                    $"   LEFT JOIN Logins AS L ON U.UserLogin_LoginId  = L.LoginId " +
                    $"   LEFT JOIN Organizations AS O ON U.OrganizationId  = O.OrganizationId " +
                    $"   LEFT JOIN UserRoles AS UR ON U.UserId  = UR.User_UserId " +
                    $"   LEFT JOIN Roles AS R ON UR.Role_RoleId  = R.RoleId " +
                    $" WHERE U.Name='abc' ;";
                    //$" WHERE U.UserId = @UserId";
                var x = "Select * from Users where Name='abc'";
                var result1 = await service.GetDataAsync(connectionString, sqlStatement);

                var result = await service.GetDataAsync<User>(connectionString, sqlStatement);

                Console.WriteLine(result.UserId);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
