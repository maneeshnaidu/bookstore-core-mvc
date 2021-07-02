using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Enum
{
    public enum LanguageEnum
    {
        [Display(Name = "English")]
        English = 1,
        [Display(Name = "Hindi")]
        Hindi = 2,
        [Display(Name = "Chinese")]
        Chinese = 3,
        [Display(Name = "German")]
        German = 4,
        [Display(Name = "French")]
        French = 5
    }
}
