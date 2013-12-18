using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Models.Document
{
    public class EditableContent
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Past some text here")]
        [StringLength(2500)]
        public string Text { get; set; }

        public int? PartId { get; set; }

        [Required(ErrorMessage = "Specify method for splitting the text")]
        public int? SplitType { get; set; }

        public string NewPartName { get; set; }

    }
}