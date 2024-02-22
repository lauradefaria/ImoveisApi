namespace AdaTech.ImoveisApi.Models
{
    public class Imovel
    {
        public Guid Id { get; set; }
        public string Endereco { get; set; }
        public string Proprietario { get; set; }

        public Imovel(Guid id, string endereco, string proprietario)
        {
            Id = id;
            Endereco = endereco;
            Proprietario = proprietario;
        }
    }
}
