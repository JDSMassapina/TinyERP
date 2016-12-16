namespace App.Service.Security.UserGroup
{
    using App.Common.Data;
    using App.Common.Mapping;
    using System;
    using System.Collections.Generic;

    public class UpdateUserGroupRequest : BaseContent, IMappedFrom<App.Entity.Security.UserGroup>
    {
        public IList<Guid> Permissions { get; set; }
        public UpdateUserGroupRequest() : base()
        {
            this.Permissions = new List<Guid>();
        }

        public UpdateUserGroupRequest(Guid id, string name, string key, string desc) : base()
        {
            this.Id = id;
            this.Name = name;
            this.Key = key;
            this.Description = desc;
            this.Permissions = new List<Guid>();
        }
    }
}
