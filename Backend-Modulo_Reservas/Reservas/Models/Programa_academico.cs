﻿using System.ComponentModel.DataAnnotations;

namespace Reservas.Models
{
    public class Programa_academico
    {
        [Key]
        public int Id_programa_academico { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}
