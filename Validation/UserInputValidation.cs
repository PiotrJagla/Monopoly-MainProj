using Models;

namespace Validation
{
    public class UserInputValidation
    {
        public static bool IsDataToRegisterCorrect(UserLoginData DataToRegister)
        {
            return DataToRegister.Name.Length <= 30 && DataToRegister.Password.Length <= 30;
        }
    }
}