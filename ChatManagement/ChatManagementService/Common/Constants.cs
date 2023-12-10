namespace ChatManagementService.Common;

public static class Constants
{
    public const string API_ADMIN_NOT_FOUND_EXCEPTION = "Specified admin does not exist";
    public const string API_USER_NOT_FOUND_EXCEPTION = "Specified user does not exist";
    public const string API_USER_ALREADY_IN_GROUP_EXCEPTION = "User is already part of this group";
    public const string API_GROUP_NOT_FOUND_EXCEPTION = "Specified group does not exist";
    public const string API_GROUP_MEMBERS_MUST_BE_CLIENTS_EXCEPTION = "All group members must be clients";
}
