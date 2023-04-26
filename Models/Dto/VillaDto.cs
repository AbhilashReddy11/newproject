
using System.ComponentModel.DataAnnotations;

namespace newproject.Models.Dto;

    public class VillaDTO
    {
        public int Id{ get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Place { get; set; }
    }
