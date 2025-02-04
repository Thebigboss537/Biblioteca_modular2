﻿namespace Catalogo.Models.Dto
{
    public class MaterialDto
    {
        public int Id_material { get; set; }

        public string ?Titulo { get; set; }

        public int Id_tipo_material { get; set; }

        public Tipo_material ?Tipo_material { get; set; }

        public int Id_editorial { get; set; }
        public Editorial ?Editorial { get; set; }

        public string ?Descripcion { get; set; }

        public string ?Año { get; set; }

        public string ?Formato { get; set; }

        public string ?Idioma { get; set; }

        public string ?ISBN { get; set; }

        public int Id_sede { get; set; }

        public Sede ?Sede { get; set; }

        public string ?Observacion { get; set; }

        public string ?Archivo { get; set; }

        public string ?nombresdeautores { get; set; }

        public string ?nombresdecategorias { get; set; }

        public List<AutorDto> ?Autores { get; set; }

        public List<CategoriaDto> ?Categorias { get; set; }

    }
}
