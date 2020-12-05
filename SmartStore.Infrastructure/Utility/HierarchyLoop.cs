using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Model.Entities;

namespace SmartStore.Infrastructure.Utility
{
    public static class HierarchyLoop
    {
        public static string GetProductGroupHierarchy(ProductGroup entity, int? selectedItemParent = null, int? selectedItem = null)
        {
            selectedItemParent = selectedItemParent ?? 0;
            selectedItem = selectedItem ?? 0;
            var content = string.Empty;
            if (entity.Id == selectedItemParent)
                content += "<li data-jstree='{ \"selected\" : true,\"opened\": true }' id=\"pg_" + entity.Id + "\">" + entity.GroupName;
            else if (entity.Id == selectedItem)
                content += "<li data-jstree='{ \"disabled\" : true }' id=\"pg_" + entity.Id + "\">" + entity.GroupName;
            else
                content += $"<li id='pg_{entity.Id}'>{entity.GroupName}";

            if (entity.Children.Any())
            {
                entity.Children.ToList().ForEach(item =>
                {
                    if (item.IsDeleted == false || item.IsDeleted == null)
                    {
                        content += "<ul>" + GetProductGroupHierarchy(item, selectedItemParent, selectedItem) + "</ul>";
                    }
                });
            }
            content += "</li>";

            return content;
        }
    }
}
