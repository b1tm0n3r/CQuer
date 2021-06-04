using AutoMapper;
using Common.DataModels.StandardEntities;
using Common.DTOs;

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
