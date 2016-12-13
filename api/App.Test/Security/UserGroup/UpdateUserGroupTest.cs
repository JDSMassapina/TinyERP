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
    public class UpdateUserGroupTest : BaseUnitTest
    {
        private CreateUserGroupResponse userGroup;
        private CreateUserGroupResponse userGroup2;
        protected override void OnInit()
        {
            base.OnInit();
            string name = "Name of User Group" + Guid.NewGuid().ToString("N");
            string desc = "Desc of User Group";
            this.userGroup = this.CreateUserGroupItem(name, desc);

            name = "Duplicated Name" + Guid.NewGuid().ToString("N");
            desc = "Desc of User Group";
            this.userGroup2 = this.CreateUserGroupItem(name, desc);
        }

        [TestMethod]
        public void Security_UserGroup_UpdateUserGroup_ShouldBeSuccess_WithValidRequest()
        {
            string name = "New Name of User Group" + Guid.NewGuid().ToString("N");
            string desc = "New Desc of User Group";
            this.UpdateUserGroupItem(this.userGroup.Id, name, desc);
            IUserGroupService service = IoC.Container.Resolve<IUserGroupService>();
            App.Service.Security.UserGroup.GetUserGroupResponse updatedGroupService = service.GetUserGroup(this.userGroup.Id);
            Assert.AreEqual(updatedGroupService.Name, name);
        }

        [TestMethod]
        public void Security_UserGroup_UpdateUserGroup_ShouldGetException_WithEmptyName()
        {
            try
            {
                string name = string.Empty;
                string desc = "Desc of User Group";
                this.UpdateUserGroupItem(this.userGroup.Id, name, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addOrUpdateUserGroup.validation.nameIsRequired"));
            }
        }

        [TestMethod]
        public void Security_UserGroup_UpdateUserGroup_ShouldGetException_WithDuplicatedKey()
        {
            try
            {
                string newGuid = Guid.NewGuid().ToString("N");
                string name = "Name of User Group" + newGuid;
                string name1 = "Name_of_User_Group" + newGuid;
                string desc = "Desc of User Group";
                this.UpdateUserGroupItem(this.userGroup.Id, name, desc);
                this.UpdateUserGroupItem(this.userGroup2.Id, name1, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addOrUpdateUserGroup.validation.keyAlreadyExisted"));
            }
        }

        [TestMethod]
        public void Security_UserGroup_UpdateUserGroup_ShouldGetException_WithEmptyId()
        {
            try
            {
                string name = "Name of User Group" + Guid.NewGuid().ToString("N");
                string desc = "Desc of User Group";
                this.UpdateUserGroupItem(Guid.Empty, name, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.userGroups.validation.userGroupIdIsInvalid"));
            }
        }

        [TestMethod]
        public void Security_UserGroup_UpdateUserGroup_ShouldGetException_WithNotExistedId()
        {
            try
            {
                string name = "Name of User Group" + Guid.NewGuid().ToString("N");
                string desc = "Desc of User Group";
                this.UpdateUserGroupItem(Guid.NewGuid(), name, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.userGroups.validation.userGroupNotExisted"));
            }
        }

        private CreateUserGroupResponse CreateUserGroupItem(string name, string desc)
        {
            CreateUserGroupRequest request = new CreateUserGroupRequest(name, desc);
            IUserGroupService service = IoC.Container.Resolve<IUserGroupService>();
            return service.Create(request);
        }

        private void UpdateUserGroupItem(Guid id, string name, string desc)
        {
            UpdateUserGroupRequest request = new UpdateUserGroupRequest(id, name, desc);
            IUserGroupService service = IoC.Container.Resolve<IUserGroupService>();
            service.Update(request);
        }
    }
}
