using AutoMapper;
using Common.DataModels.StandardEntities;
using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.MappingProfiles
{
    public class FileReferenceMapperProfile : Profile
    {
        public FileReferenceMapperProfile()
        {
            CreateMap<FileReference, FileReferenceDto>();
        }
    }
}
