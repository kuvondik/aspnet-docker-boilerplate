using AspNetDockerBoilerplate.Models.DomainModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace AspNetDockerBoilerplate.Builders
{
    public class BasicObjectRepository
    {
        private readonly ILogger<BasicObjectRepository> _logger;
        private static List<BasicObject> _basicObjList;

        public static BasicObject BasicObjectRepLica = new BasicObject { Value = "New Value" };

        public BasicObjectRepository(ILogger<BasicObjectRepository> logger)
        {
            _logger = logger;
            _basicObjList = new List<BasicObject>
            {
                new BasicObject
                {
                    Key = "key1",
                    Value = "White",
                },
                new BasicObject
                {
                    Key = "key2",
                    Value = "Black",
                },
                new BasicObject
                {
                    Key = "key3",
                    Value = "Red",
                }
            };
        }

        public List<BasicObject> GetAll()
        {
            if (_basicObjList == null)
            {
                _logger.LogWarning($"\nObject list is null!\n");
                return null;
            }

            if (!_basicObjList.Any())
            {
                _logger.LogWarning($"\nObject list is empty!\n");
                return null;
            }

            return _basicObjList;
        }

        public BasicObject GetByKey(string key)
        {
            var obj = _basicObjList.Find(obj => obj.Key == key);

            if (obj == null)
            {
                _logger.LogError($"\nObject (with key: {key}) couldn't be found!\n");
                return null;
            }

            return obj;
        }

        public BasicObject Update(BasicObject basicObject)
        {
            var objIndex = _basicObjList.FindIndex(obj => obj.Key == basicObject.Key);

            if (objIndex < 0)
            {
                _logger.LogError($"\nObject (with key: {basicObject.Key}) couldn't be updated!\n");
                return null;
            }

            _basicObjList[objIndex].Value = basicObject.Value;
            _logger.LogInformation($"\nObject (with key: {basicObject.Key}) is updated successfully!\n");

            return _basicObjList[objIndex];
        }

        public BasicObject Create(BasicObject basicObject)
        {
            if (_basicObjList == null)
                _basicObjList = new List<BasicObject>();

            var obj = new BasicObject
            {
                Value = basicObject.Value,
                Key = "key" + _basicObjList.Count
            };

            _basicObjList.Add(obj);
            _logger.LogInformation($"\nObject is created with key: {obj.Key}\n");

            return obj;
        }

        public BasicObject Delete(string key)
        {
            var objToRemove = _basicObjList.Find(obj => obj.Key == key);

            if (objToRemove == null)
            {
                _logger.LogError($"\nObject (with key: {key}) couldn't be found!\n");
                return null;
            }

            _basicObjList.Remove(objToRemove);
            _logger.LogInformation($"\nObject (with key: {key}) has been deleted successfully!\n");

            return objToRemove;
        }
    }
}
