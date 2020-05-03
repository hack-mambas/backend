namespace Healfi.Api.Application.Common
{
    public class FailViewModel
    {
        public string Causa { get; set; }
        public string Erros { get; set; }

        public FailViewModel(string causa, string erros)
        {
            Causa = causa;
            Erros = erros;
        }

        public FailViewModel(string causa)
        {
            this.Causa = causa;
        }
    }
}