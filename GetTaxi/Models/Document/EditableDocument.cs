using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Document
{
    public class EditableDocument
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Specify the original language of document")]
        public int? OriginalLanguage { get; set; }

        [Required(ErrorMessage = "Specify the language of translation")]
        public int? TranslateLangiage { get; set; }
    }
}