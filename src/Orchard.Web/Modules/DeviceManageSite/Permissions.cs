using Orchard.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions.Models;

namespace DeviceManageSite
{
    public class Permissions : IPermissionProvider
    {

        public static readonly Permission ResourceAdmin = new Permission { Name = "ResourceAdmin", Description = "Manage All Of Resource Component",ImpliedBy = new[] { ResourceBasic} };
        public static readonly Permission ResourceBasic = new Permission { Name = "ResourceBasic", Description = "Edit,Update Resource" };
        public Feature Feature
        {
            get;set;
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name ="ResourceBasic",
                    Permissions = new [] {ResourceBasic}
                },
                new PermissionStereotype
                {
                    Name="ResourceAdmin",
                    Permissions = new [] {ResourceAdmin}
                }
            };
        }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                ResourceAdmin,
                ResourceBasic
            };
        }
    }
}