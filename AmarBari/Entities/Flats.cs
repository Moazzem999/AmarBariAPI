﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AmarBari.Entities
{
    public class Flats
    {
        [Key]
        public long Id { get; set; }
        public long BuildingId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Description { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Floor { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }


        [ForeignKey("BuildingId")]
        public virtual Buildings Building { get; set; } = null!;
        public virtual List<Renters> Renters { get; set; }
    }
}
