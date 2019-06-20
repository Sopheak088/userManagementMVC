using Repository.Pattern.Ef6;
using System;
using System.ComponentModel.DataAnnotations;

namespace iThinking.UserCenter.Common
{
    public interface IBaseObject
    {
        void New(string userName);

        void Update(string userName);
    }

    public abstract class BaseObject : Entity, IBaseObject
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

        public BaseObject()
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
