namespace ApiProject1.Controllers
{
    internal class StringResult
    {
        public string resultString;
        public string message;

        public StringResult(string resultString, string mensaje )
        {
            this.resultString = resultString;
            this.message = mensaje ;
        }
    }
}