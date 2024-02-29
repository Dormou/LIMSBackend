using AutoMapper;
using testing_managment.DataTransferObjects.CaseInBlockDto;
using testing_managment.DataTransferObjects.CompanyDto;
using testing_managment.DataTransferObjects.DutDto;
using testing_managment.DataTransferObjects.TestBlockDto;
using testing_managment.DataTransferObjects.TestProgramDto;
using testing_managment.Models.Entities;

namespace testing_managment.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CaseInBlock, CaseInBlockViewDto>();
            CreateMap<CaseInBlockCreateDto, CaseInBlock>();

            CreateMap<Dut, DutViewDto>();
            CreateMap<DutCreateDto, Dut>();

            CreateMap<Company, CompanyViewDto>();
            CreateMap<CompanyCreateDto, Company>();

            CreateMap<TestBlock, TestBlockViewDto>();
            CreateMap<TestBlockCreateDto, TestBlock>();

            CreateMap<TestProgram, TestProgramViewDto>();
            CreateMap<TestProgramCreateDto, TestProgram>();
        }
    }
}
