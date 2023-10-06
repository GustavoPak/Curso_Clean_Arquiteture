using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This name is Required")]
        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
