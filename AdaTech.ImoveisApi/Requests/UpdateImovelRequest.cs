using System.ComponentModel.DataAnnotations;

namespace AdaTech.ImoveisApi.Requests
{
    public class UpdateImovelRequest
    {
        public UpdateImovelRequest(string proprietario, string endereco) 
        {
            Proprietario = proprietario;
            Endereco = endereco;
        }

        [Required(ErrorMessage = "O campo 'Proprietario' é obrigatório.")]
         public string Proprietario { get; set; }
        [Required(ErrorMessage = "O campo 'Proprietario' é obrigatório.")]
         public string Endereco { get; set;}
    }
}
