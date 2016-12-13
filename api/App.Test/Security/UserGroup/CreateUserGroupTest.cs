namespace App.Test.Security.UserGroup
{
    using App.Common.DI;
    using App.Common.UnitTest;
    using App.Common.Validation;
    using App.Service.Security;
    using App.Service.Security.UserGroup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class CreateUserGroupTest : BaseUnitTest
    {
        [TestMethod]
        public void Security_UserGroup_CreateUserGroup_ShouldBeSuccess_WithValidRequest()
        {
            string name = "Name of User Group" + Guid.NewGuid().ToString("N");
            string desc = "Desc of User Group";
            CreateUserGroupResponse userGroup = this.CreateUserGroupItem(name, desc);
            IUserGroupService service = IoC.Container.Resolve<IUserGroupService>();
            Assert.IsNotNull(service.GetUserGroup(userGroup.Id));
        }

        [TestMethod]
        public void Security_UserGroup_CreateUserGroup_ShouldGetException_WithEmptyName()
        {
            try
            {
                string name = string.Empty;
                string desc = "Desc of User Group";
                this.CreateUserGroupItem(name, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addOrUpdateUserGroup.validation.nameIsRequired"));
            }
        }

        [TestMethod]
        public void Security_UserGroup_CreateUserGroup_ShouldGetException_WithDuplicatedName()
        {
            try
            {
                string name = "Duplicated Name" + Guid.NewGuid().ToString("N");
                string desc = "Desc of User Group";
                this.CreateUserGroupItem(name, desc);
                this.CreateUserGroupItem(name, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addOrUpdateUserGroup.validation.nameAlreadyExisted"));
            }
        }

        private CreateUserGroupResponse CreateUserGroupItem(string name, string desc)
        {
            CreateUserGroupRequest request = new CreateUserGroupRequest(name, desc);
            IUserGroupService service = IoC.Container.Resolve<IUserGroupService>();
            return service.Create(request);
        }
    }
}
