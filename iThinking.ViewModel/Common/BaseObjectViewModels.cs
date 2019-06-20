using System;
using System.ComponentModel.DataAnnotations;

namespace iThinking.ViewModel.Common
{
    public class BaseObjectViewModel
    {
        [MaxLength(256)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created date")]
        public DateTime CreatedDate { get; set; }

        [MaxLength(256)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Updated date")]
        public DateTime? UpdatedDate { get; set; }

        public BaseObjectViewModel()
        {
            CreatedDate = DateTime.Now;
        }

        public void New(string userName)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = userName;
        }

        public void Update(string userName)
        {
            UpdatedBy = userName;
            UpdatedDate = DateTime.Now;
        }
    }
}