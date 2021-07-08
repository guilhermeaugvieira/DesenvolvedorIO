namespace CursoEFCore.Domain
{
    // [Table("Cliente")]
    public class Cliente
    {
        // [Key]
        public int Id { get; set; }
        // [Required]
        public string Nome { get; set; }
        // [Column("Telefone")]
        public string Telefone { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
    }
}
