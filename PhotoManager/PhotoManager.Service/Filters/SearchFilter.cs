
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PhotoManager.Service.Contract.DataContract;
using Photo = PhotoManager.Service.Data.Model.Photo;

namespace PhotoManager.Service.Filters
{
    public class SearchFilter
    {
        public static IEnumerable<Expression<Func<Photo, bool>>> GetFilters(PhotoResponse source)
        {
            var filters = new List<Expression<Func<Photo, bool>>>();

            if(!String.IsNullOrEmpty(source.Name))
                filters.Add(e => e.Name.Contains(source.Name.Trim()));

            if (!String.IsNullOrEmpty(source.Description))
                filters.Add(e => e.Description.Contains(source.Description.Trim()));

            if (!String.IsNullOrEmpty(source.PlaceCreated))
                filters.Add(e => e.PlaceCreated.ToUpper().Contains(source.PlaceCreated.ToUpper().Trim()));

            if (!String.IsNullOrEmpty(source.CameraModel))
                filters.Add(e => e.CameraModel.ToUpper().Contains(source.CameraModel.ToUpper().Trim()));

            if (!String.IsNullOrEmpty(source.FocalLengthOfTheLens))
                filters.Add(e => e.FocalLengthOfTheLens.Contains(source.FocalLengthOfTheLens.Trim()));

            if (!String.IsNullOrEmpty(source.Diaphragm))
                filters.Add(e => e.Diaphragm.ToUpper().Contains(source.Diaphragm.ToUpper().Trim()));

            if (!String.IsNullOrEmpty(source.ShutterSpeed))
                filters.Add(e => e.ShutterSpeed.Contains(source.ShutterSpeed.Trim()));

            if (!String.IsNullOrEmpty(source.ISO))
                filters.Add(e => e.ISO.ToUpper().Contains(source.ISO.ToUpper().Trim()));

            if (source.FlashInUse)
                filters.Add(e => e.FlashInUse);

            return filters;
        }
    }
}