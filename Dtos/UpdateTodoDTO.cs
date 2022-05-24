using System.ComponentModel.DataAnnotations;

namespace MeuTodo.Dtos
{
    public class UpdateTodoDTO
    {
        [Required]
        public string Title { get; set; }
    }
}