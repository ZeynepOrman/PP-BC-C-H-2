namespace PP_BC_C_H_2.Services
{
    public interface IFakeService
    {
        bool ValidateUser(string username, string password);
    }

    public class FakeService : IFakeService
    {
        public bool ValidateUser(string username, string password)
        {
            // Fake user validation logic
            return username == "test" && password == "password";
        }
    }
}
