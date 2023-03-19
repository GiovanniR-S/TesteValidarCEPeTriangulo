namespace CEP.Models {
    public class CEPS {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public long Unidade { get; set; }
        public int IBGE { get; set; }
        public string GIA { get; set; }
    }
}
