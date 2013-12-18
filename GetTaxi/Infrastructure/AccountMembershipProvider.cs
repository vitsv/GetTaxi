using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebUI.Infrastructure
{
    public class AccountMembershipProvider : MembershipProvider
    {
        ////TODO zastosowac IoC
        //public WorkerService AccountRepository { get; set; }

        //public AccountMembershipProvider()
        //{
        //    this.AccountRepository = new WorkerService(new EODContext());
        //}

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        //
        // Summary:
        //     Indicates whether the membership provider is configured to allow users to
        //     reset their passwords.
        //
        // Returns:
        //     true if the membership provider supports password reset; otherwise, false.
        //     The default is true.
        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        //
        // Summary:
        //     Indicates whether the membership provider is configured to allow users to
        //     retrieve their passwords.
        //
        // Returns:
        //     true if the membership provider is configured to support password retrieval;
        //     otherwise, false. The default is false.
        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        //
        // Summary:
        //     Gets the number of invalid password or password-answer attempts allowed before
        //     the membership user is locked out.
        //
        // Returns:
        //     The number of invalid password or password-answer attempts allowed before
        //     the membership user is locked out.
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return 3;
            }
        }
        //
        // Summary:
        //     Gets the minimum number of special characters that must be present in a valid
        //     password.
        //
        // Returns:
        //     The minimum number of special characters that must be present in a valid
        //     password.
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        //
        // Summary:
        //     Gets the minimum length required for a password.
        //
        // Returns:
        //     The minimum length required for a password.
        public override int MinRequiredPasswordLength
        {
            get
            {
                return 8;
            }
        }
        //
        // Summary:
        //     Gets the number of minutes in which a maximum number of invalid password
        //     or password-answer attempts are allowed before the membership user is locked
        //     out.
        //
        // Returns:
        //     The number of minutes in which a maximum number of invalid password or password-answer
        //     attempts are allowed before the membership user is locked out.
        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        //
        // Summary:
        //     Gets a value indicating the format for storing passwords in the membership
        //     data store.
        //
        // Returns:
        //     One of the System.Web.Security.MembershipPasswordFormat values indicating
        //     the format for storing passwords in the data store.
        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        //
        // Summary:
        //     Gets the regular expression used to evaluate a password.
        //
        // Returns:
        //     A regular expression used to evaluate a password.
        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        //
        // Summary:
        //     Gets a value indicating whether the membership provider is configured to
        //     require the user to answer a password question for password reset and retrieval.
        //
        // Returns:
        //     true if a password answer is required for password reset and retrieval; otherwise,
        //     false. The default is true.
        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        //
        // Summary:
        //     Gets a value indicating whether the membership provider is configured to
        //     require a unique e-mail address for each user name.
        //
        // Returns:
        //     true if the membership provider requires a unique e-mail address; otherwise,
        //     false. The default is true.
        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        // Summary:
        //     Processes a request to update the password for a membership user.
        //
        // Parameters:
        //   username:
        //     The user to update the password for.
        //
        //   oldPassword:
        //     The current password for the specified user.
        //
        //   newPassword:
        //     The new password for the specified user.
        //
        // Returns:
        //     true if the password was updated successfully; otherwise, false.
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Processes a request to update the password question and answer for a membership
        //     user.
        //
        // Parameters:
        //   username:
        //     The user to change the password question and answer for.
        //
        //   password:
        //     The password for the specified user.
        //
        //   newPasswordQuestion:
        //     The new password question for the specified user.
        //
        //   newPasswordAnswer:
        //     The new password answer for the specified user.
        //
        // Returns:
        //     true if the password question and answer are updated successfully; otherwise,
        //     false.
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Adds a new membership user to the data source.
        //
        // Parameters:
        //   username:
        //     The user name for the new user.
        //
        //   password:
        //     The password for the new user.
        //
        //   email:
        //     The e-mail address for the new user.
        //
        //   passwordQuestion:
        //     The password question for the new user.
        //
        //   passwordAnswer:
        //     The password answer for the new user
        //
        //   isApproved:
        //     Whether or not the new user is approved to be validated.
        //
        //   providerUserKey:
        //     The unique identifier from the membership data source for the user.
        //
        //   status:
        //     A System.Web.Security.MembershipCreateStatus enumeration value indicating
        //     whether the user was created successfully.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object populated with the information
        //     for the newly created user.
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Removes a user from the membership data source.
        //
        // Parameters:
        //   username:
        //     The name of the user to delete.
        //
        //   deleteAllRelatedData:
        //     true to delete data related to the user from the database; false to leave
        //     data related to the user in the database.
        //
        // Returns:
        //     true if the user was successfully deleted; otherwise, false.
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Gets a collection of membership users where the e-mail address contains the
        //     specified e-mail address to match.
        //
        // Parameters:
        //   emailToMatch:
        //     The e-mail address to search for.
        //
        //   pageIndex:
        //     The index of the page of results to return. pageIndex is zero-based.
        //
        //   pageSize:
        //     The size of the page of results to return.
        //
        //   totalRecords:
        //     The total number of matched users.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection collection that contains a
        //     page of pageSizeSystem.Web.Security.MembershipUser objects beginning at the
        //     page specified by pageIndex.
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Gets a collection of membership users where the user name contains the specified
        //     user name to match.
        //
        // Parameters:
        //   usernameToMatch:
        //     The user name to search for.
        //
        //   pageIndex:
        //     The index of the page of results to return. pageIndex is zero-based.
        //
        //   pageSize:
        //     The size of the page of results to return.
        //
        //   totalRecords:
        //     The total number of matched users.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection collection that contains a
        //     page of pageSizeSystem.Web.Security.MembershipUser objects beginning at the
        //     page specified by pageIndex.
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Gets a collection of all the users in the data source in pages of data.
        //
        // Parameters:
        //   pageIndex:
        //     The index of the page of results to return. pageIndex is zero-based.
        //
        //   pageSize:
        //     The size of the page of results to return.
        //
        //   totalRecords:
        //     The total number of matched users.
        //
        // Returns:
        //     A System.Web.Security.MembershipUserCollection collection that contains a
        //     page of pageSizeSystem.Web.Security.MembershipUser objects beginning at the
        //     page specified by pageIndex.
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Gets the number of users currently accessing the application.
        //
        // Returns:
        //     The number of users currently accessing the application.
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Gets the password for the specified user name from the data source.
        //
        // Parameters:
        //   username:
        //     The user to retrieve the password for.
        //
        //   answer:
        //     The password answer for the user.
        //
        // Returns:
        //     The password for the specified user name.
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Gets user information from the data source based on the unique identifier
        //     for the membership user. Provides an option to update the last-activity date/time
        //     stamp for the user.
        //
        // Parameters:
        //   providerUserKey:
        //     The unique identifier for the membership user to get information for.
        //
        //   userIsOnline:
        //     true to update the last-activity date/time stamp for the user; false to return
        //     user information without updating the last-activity date/time stamp for the
        //     user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object populated with the specified
        //     user's information from the data source.
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Gets information from the data source for a user. Provides an option to update
        //     the last-activity date/time stamp for the user.
        //
        // Parameters:
        //   username:
        //     The name of the user to get information for.
        //
        //   userIsOnline:
        //     true to update the last-activity date/time stamp for the user; false to return
        //     user information without updating the last-activity date/time stamp for the
        //     user.
        //
        // Returns:
        //     A System.Web.Security.MembershipUser object populated with the specified
        //     user's information from the data source.
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Gets the user name associated with the specified e-mail address.
        //
        // Parameters:
        //   email:
        //     The e-mail address to search for.
        //
        // Returns:
        //     The user name associated with the specified e-mail address. If no match is
        //     found, return null.
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Resets a user's password to a new, automatically generated password.
        //
        // Parameters:
        //   username:
        //     The user to reset the password for.
        //
        //   answer:
        //     The password answer for the specified user.
        //
        // Returns:
        //     The new password for the specified user.
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        //
        // Summary:
        //     Clears a lock so that the membership user can be validated.
        //
        // Parameters:
        //   userName:
        //     The membership user whose lock status you want to clear.
        //
        // Returns:
        //     true if the membership user was successfully unlocked; otherwise, false.
        public override bool UnlockUser(string userName)
        {
            using (var AccountRepository = new UserManager())
            {
                var res = AccountRepository.UnlockUser(userName);
                if (res.IsError)
                    return false;
                else
                    return true;
            }
        }
        //
        // Summary:
        //     Updates information about a user in the data source.
        //
        // Parameters:
        //   user:
        //     A System.Web.Security.MembershipUser object that represents the user to update
        //     and the updated information for the user.
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Verifies that the specified user name and password exist in the data source.
        //
        // Parameters:
        //   username:
        //     The name of the user to validate.
        //
        //   password:
        //     The password for the specified user.
        //
        // Returns:
        //     true if the specified username and password are valid; otherwise, false.
        public override bool ValidateUser(string username, string password)
        {
            using (var AccountRepository = new UserManager())
            {
                return AccountRepository.IsValidPhone(username, password);
            }
        }




    }
}