using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;
using System.Linq;

namespace SmartStore.Classes
{
    public static class SubsetHierarchy
    {
        public static string GetChildrenChart(int parentId,bool isMainSubset)
        {
            using (var db = new SmartStoreContext())
            {
            var parent = db.Users.Find(parentId);
            var userProfitManager = new UserProfitManager(db);
            var userPoints = userProfitManager.GetUserPointsAmount(parent);
            var content = $"<li><div class='user'><img src= '/Images/User/{parent.UserImage ?? "user-avatar.png"}' class='img-responsive' />" +
                $"<div class='name'>{parent.UserFirstName} {parent.UserLastName}</div><div class='role'>{parent.Role.RoleTitle}</div><div class='role'>Points: {userPoints}</div>";
            
            if(isMainSubset) content += $"<button class='btn btn-sm btn-danger' onclick='btnRemoveSubset({parent.Id})'>Remove Subset</button>";

            content +="</div>";
                var children = db.Users.Where(u => u.UserIdentifierCode == parent.UserCode).ToList();
                if (children.Any())
                {
                    content += "<ul>";
                    children.ToList().ForEach(child =>
                    {
                        if (child.IsActive == true)
                        {
                            content += GetChildrenChart(child.Id,false);
                        }
                    });
                    content += "</ul>";
                }
                content += "</li>";
                return content;
            }
        }
    }
}
