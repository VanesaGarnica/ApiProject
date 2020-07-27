using System.Security.Cryptography.X509Certificates;

namespace ApiProject1.Controllers
{
    internal class IntegerResult
    {
        public int resultInt;
        public string message;
        public IntegerResult(int resultInt, string message)
        {
            this.resultInt = resultInt;
            this.message = message;
        }
    }
}