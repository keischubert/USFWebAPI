﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using USFWebAPI.Validations;

namespace USFWebAPI.Entities
{
    public class Gender
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "{0} no puede exceder {1} caracteres")]
        [FirstLetterUpperCase]
        [NoWhiteSpace]
        public string Name { get; set; }
    }
}
