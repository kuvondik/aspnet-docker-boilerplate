using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetDockerBoilerplate.Models.ApiModels
{
    public class BasicObjectApiModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
