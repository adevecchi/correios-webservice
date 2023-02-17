using System.ComponentModel.DataAnnotations;

namespace BuscaCep.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }

        [Required(ErrorMessage = "CEP é obrigatório")]
        [RegularExpression(@"\d{5}-\d{3}", ErrorMessage = "Digite o CEP no formato 99999-999")]
        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public int Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Complemento { get; set; }
    }
}
