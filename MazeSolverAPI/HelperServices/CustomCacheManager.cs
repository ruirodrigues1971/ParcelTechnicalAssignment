using Domain;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Linq;

namespace MazeSolverAPI.HelperServices
{
    public class CustomCacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, object> _cacheKeysValues;

        public CustomCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheKeysValues = new ConcurrentDictionary<string, object>();
        }

        public void Set<T>(string key, T value, MemoryCacheEntryOptions options = null)
        {
            _memoryCache.Set(key, value, options);
            _cacheKeysValues[key] = value;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public Dictionary<string, object> GetAllKeysValues()
        {
            return new Dictionary<string, object>(_cacheKeysValues);
        }

        public IEnumerable<IntegratedSolution> GetSolutions()
        {
            List<IntegratedSolution> result = [];
            var keyValues = _cacheKeysValues.ToArray();

            foreach (var value in keyValues)
            {
                if (value.Value is MazeSolution solution)
                {
                    result.Add(
                        new IntegratedSolution(){
                            Puzzle = value.Key,
                            Solution = solution
                        });
                }
            }
            return result;
        }
    }
}
