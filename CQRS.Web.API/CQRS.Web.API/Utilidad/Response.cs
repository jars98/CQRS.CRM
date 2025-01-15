﻿using CQRS.Web.API.Application.Validator;

namespace CQRS.Web.API.Utilidad
{
    public class Response<T>
    {
        public bool status { get; set; }    
        public T value { get; set; } 
        public string msg { get; set; }
        public List<ValidationError> errors { get; set; }
    }
}
