namespace Blazor.Client.Utilities
{
    public static class AuthorizationChecker
    {
        public static bool IsAuthorizedToOrder(string loggedInUserId, Guid userIdInCart)
        {
            var loggedInUserIdAsGuid = Guid.Parse(loggedInUserId.Trim('"'));
            if (loggedInUserIdAsGuid != userIdInCart)
            {
                return false;
            }
            return true;
        }

        public static bool IsAuthorizedToSeeOrder(string loggedInUserId, Guid userIdInOrder)
        {
            var loggedInUserIdAsGuid = Guid.Parse(loggedInUserId.Trim('"'));
            if (loggedInUserIdAsGuid != userIdInOrder)
            {
                return false;
            }
            return true;
        }

        public static bool IsAuthorizedToMakeChangesInCart(string loggedInUserEmail, string userEmailInClaims)
        {
            if (loggedInUserEmail.Trim('"') != userEmailInClaims.Trim('"'))
            {
                return false;
            }
            return true;
        }
    }
}
