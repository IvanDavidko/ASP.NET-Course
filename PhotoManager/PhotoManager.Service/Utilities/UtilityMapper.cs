
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace PhotoManager.Service.Utilities
{
    public class UtilityMapper
    {
        public T ObjectMapper<T, TS>(TS source)
        {
            return (T) Mapper.Map(source, source.GetType(), typeof (T));
        }

        public List<T> ListMapper<T, TS>(IEnumerable<TS> sourceList)
        {
            var mappedList = new List<T>();

            sourceList.ToList().ForEach(sourceObject => mappedList.Add((T)ObjectMapper<T, TS>(sourceObject)));

            return mappedList;
        }
    }
}