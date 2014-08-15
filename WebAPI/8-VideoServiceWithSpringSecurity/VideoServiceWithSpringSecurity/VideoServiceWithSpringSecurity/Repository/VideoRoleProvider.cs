using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoServiceWithSpringSecurity.Repository
{
    public class VideoRoleProvider : System.Web.Security.RoleProvider
    {
        private string _applicationName;

        private IVideoRepository _repository;

        public IVideoRepository Repository
        {
            get
            {
                return _repository;
            }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            var videoRepositoryType = config["VideoRepositoryType"];
            if (!String.IsNullOrEmpty(videoRepositoryType))
            {
                var type = Type.GetType(videoRepositoryType);
                _repository = (IVideoRepository)Activator.CreateInstance(type);
            }
        }

        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }


        public override bool IsUserInRole(string username, string roleName)
        {

            return _repository.HasRole(username, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return _repository.UserRoles(username).ToArray();
        }

        #region Not implemented

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    
    }
}