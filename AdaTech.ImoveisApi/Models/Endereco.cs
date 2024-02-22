namespace AdaTech.ImoveisApi.Models
{
    public class Endereco
    {
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }

        public string EscreverEndereco()
        {
            return $"{this.Rua}, {this.Bairro} - {this.Cidade}/{this.Estado}";
        }
    }
}
