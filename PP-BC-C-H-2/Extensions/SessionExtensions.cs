using Microsoft.AspNetCore.Http;

namespace PP_BC_C_H_2.Extensions
{
    public static class SessionExtensions
    {
        public static bool IsAuthenticated(this ISession session)
        {
            return session.GetInt32("authenticationcompleted") == 1;
        }
    }
}
