namespace App.Service.Security.UserGroup
{
    public class CreateUserGroupResponse : App.Common.Data.BaseContent, App.Common.Mapping.IMappedFrom<App.Entity.Security.UserGroup>
    {
        public CreateUserGroupResponse(App.Entity.Security.UserGroup userGroup) : base(userGroup)
        {
        }
    }
}
