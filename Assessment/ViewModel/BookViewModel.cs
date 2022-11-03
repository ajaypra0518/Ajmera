using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Assessment.ViewModel
{
    public class BookViewModel
    {
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string AuthorName { get; set; }
    }
}
