using AspNetDockerBoilerplate.Builders;
using AspNetDockerBoilerplate.Models.ApiModels;
using AspNetDockerBoilerplate.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetDockerBoilerplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasicController : ControllerBase
    {
        #region Private members

        private readonly ILogger<BasicController> _logger;
        private readonly BasicObjectRepository _basicObjectRepository;
        #endregion

        #region Public Constructor

        public BasicController(ILogger<BasicController> logger, BasicObjectRepository basicObjectRepository)
        {
            _logger = logger;
            _basicObjectRepository = basicObjectRepository;
        }
        #endregion

        #region Public Methods

        [HttpPost]
        public IActionResult Create(BasicObjectApiModel basicObj)
        {
            var obj = _basicObjectRepository.Create(new BasicObject
            {
                Value = basicObj.Value,                
            });

            if (obj == null)
                return BadRequest();

            return Ok(new BasicObjectApiModel 
            {
                Key = obj.Key,
                Value = obj.Value,
                CreatedDate = obj.CreatedDate,
                ModifiedDate = obj.ModifiedDate
            });
        }
         
        [HttpPut]
        public IActionResult Update(BasicObjectApiModel basicObj)
        {
            var obj = _basicObjectRepository.GetByKey(basicObj.Key);
            
            if (obj == null)
                return BadRequest();

            obj.Value = basicObj.Value;

            var updatedObj = _basicObjectRepository.Update(obj);

            return Ok(new BasicObjectApiModel
            {
                Key = updatedObj.Key,
                Value = updatedObj.Value,
                CreatedDate = updatedObj.CreatedDate,
                ModifiedDate = updatedObj.ModifiedDate
            });
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _basicObjectRepository.GetAll();
            var basicObjList = objList.Select(obj => new BasicObjectApiModel 
            {
                Key = obj.Key,
                Value = obj.Value,
                CreatedDate = obj.CreatedDate,
                ModifiedDate = obj.ModifiedDate
            });

            return Ok(basicObjList);
        }

        [HttpGet]
        [Route("{key}")]
        public IActionResult GetByKey(string key)
        {
            var obj = _basicObjectRepository.GetByKey(key);

            if (obj == null)
                return NotFound();

            return Ok(new BasicObjectApiModel
            {
                Key = obj.Key,
                Value = obj.Value,
                CreatedDate = obj.CreatedDate,
                ModifiedDate = obj.ModifiedDate
            });
        }

        [HttpDelete]
        public IActionResult Delete(string key)
        {
            var removedObj = _basicObjectRepository.Delete(key);

            if (removedObj == null)
                return BadRequest();

            return Ok(new BasicObjectApiModel
            {
                Key = removedObj.Key,
                Value = removedObj.Value,
                CreatedDate = removedObj.CreatedDate,
                ModifiedDate = removedObj.ModifiedDate
            });
        }
        #endregion
    }
}
