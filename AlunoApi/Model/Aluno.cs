using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlunoApi.Model

{
    [Table("Alunos")]
    public class Aluno
    {   //development by Marco
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "Digite um nome de no máximo 80 caracteres!")]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public int Idade { get; set; }
    }
}
