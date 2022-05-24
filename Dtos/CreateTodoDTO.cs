using System.ComponentModel.DataAnnotations;

namespace MeuTodo.Dtos
{
    public class CreateTodoDTO
    {
        [Required]
        public string Title { get; set; }
    }
}