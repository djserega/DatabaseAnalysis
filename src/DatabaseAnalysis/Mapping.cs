using AutoMapper;
using AutoMapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAnalysis.Models;

namespace DatabaseAnalysis
{
    internal class Mapping<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {
        internal Mapping()
        {
            try
            {
                Mapper.Initialize(f => f.CreateMap<TSource, TDestination>());
            }
            catch (Exception ex)
            {
                Dialog.ShowMessage(ex.Message);
            }
        }

        internal void MappingObject(TSource source, TDestination destination)
        {
            Mapper.Map(source, destination);
        }
    }
}
