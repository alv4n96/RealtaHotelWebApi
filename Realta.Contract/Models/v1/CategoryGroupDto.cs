﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realta.Domain.Entities
{
    [Table("Category_Group")]
    public class CategoryGroupDto
    {
        [Key] public int CagroId { get; set; }
        [Required] public string? CagroName { get; set; }
        public string? CagroDescription { get; set; }
        private string? _CagroType;
        [Required]
        public string? CagroType
        {
            get { return _CagroType; }
            set
            {
                if (value == "category" || value == "service" || value == "facility")
                {
                    _CagroType = value;
                }
                else
                {
                    throw new ArgumentException("Input harus berupa category,service,facility");
                }
            }
        }
        public string? CagroIcon { get; set; }
        public string? CagroIconUrl { get; set; }
    }
}