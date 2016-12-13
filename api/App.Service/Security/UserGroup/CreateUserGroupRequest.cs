namespace App.Service.Security.UserGroup
{
    using App.Common.Data;
    using App.Common.Mapping;
    using System;
    using System.Collections.Generic;

    public class CreateUserGroupRequest : BaseContent, IMappedFrom<App.Entity.Security.UserGroup>
    {
        public IList<Guid> Permissions { get; set; }
        public CreateUserGroupRequest() : base()
        {
            this.Permissions = new List<Guid>();
        }

        public CreateUserGroupRequest(string name, string desc) : base()
        {
            this.Name = name;
            this.Description = desc;
            this.Permissions = new List<Guid>();
        }
    }
}
