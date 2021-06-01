using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetDockerBoilerplate.Models.DomainModels
{
    public delegate void ValueChangedEvent();

    public class BasicObject
    {
        #region Private Events
        
        private event ValueChangedEvent ValueChangedEvent;
        #endregion 
        

        #region Private Members

        private string _value;
        #endregion


        #region Public Constructor
        
        public BasicObject()
        {
            ValueChangedEvent += () => ModifiedDate = DateTime.UtcNow;
        }
        #endregion


        #region Public Properties

        public string Key { get; set; }
        public string Value
        {
            get => _value;
            set
            {
                if (value != _value)
                {
                    if (_value != null)
                        ValueChangedEvent();
                    else
                        this.CreatedDate = DateTime.UtcNow;

                    _value = value;                    
                }
            }
        }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        // Hidden from ApiModel
        // *just for demonstration
        public object InternalField { get; set; }
        #endregion
    }
}
