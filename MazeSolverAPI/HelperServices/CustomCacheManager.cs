using Domain;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Linq;

namespace MazeSolverAPI.HelperServices
{
    /// <summary>
    /// Custom Cache Manager
    /// </summary>
    public class CustomCacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, object> _cacheKeysValues;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoryCache"></param>
        public CustomCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheKeysValues = new ConcurrentDictionary<string, object>();
        }

        /// <summary>
        /// Set the value to cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public void Set<T>(string key, T value, MemoryCacheEntryOptions? options = null)
        {
            _memoryCache.Set(key, value, options);
            _cacheKeysValues[key] = value!;
        }

        /// <summary>
        /// Try to get the value from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue<T>(string key, out T value)
        {
            return _memoryCache.TryGetValue(key, out value!);
        }

        /// <summary>
        /// Get the keys and values from cache
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetAllKeysValues()
        {
            return new Dictionary<string, object>(_cacheKeysValues);
        }

        /// <summary>
        /// Get the solutions from cache
        /// </summary>
        /// <returns></returns>
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
