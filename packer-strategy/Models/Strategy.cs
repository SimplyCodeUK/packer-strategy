using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace packer_strategy.Models
{
    public class Strategy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Key { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }
    }
}
