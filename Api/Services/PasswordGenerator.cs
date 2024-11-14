namespace Api.Services
{
    public class PasswordGenerator
    {
        public static string GeneratePassword(int lowercase, int uppercase, int numerics, int symbols)
        {
            const string lowers = "abcdefghijklmnopqrstuvwxyz";
            const string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "0123456789";
            const string symbol = "!@#$%^&*()_-+=";

            var random = new Random();

            var generated = "!";
            for (var i = 1; i <= lowercase; i++)
            {
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );
            }

            for (var i = 1; i <= uppercase; i++)
            {
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );
            }

            for (var i = 1; i <= numerics; i++)
            {
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );
            }

            for (var i = 1; i <= symbols; i++)
            {
                generated = generated.Insert(
                    random.Next(generated.Length),
                    symbol[random.Next(symbol.Length - 1)].ToString()
                );
            }

            return generated.Replace("!", string.Empty);
        }
    }
}