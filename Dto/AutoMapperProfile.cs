using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ePTW.Models;

namespace ePTW.Dto
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();
            CreateMap<Workgroup, WorkgroupDto>();
            CreateMap<WorkgroupDto, Workgroup>();
            CreateMap<Unit, UnitDto>();
            CreateMap<UnitDto, Unit>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<Station, StationDto>();
            CreateMap<StationDto, Station>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Designation, DesignationDto>();
            CreateMap<DesignationDto, Designation>();
            CreateMap<Grade, GradeDto>();
            CreateMap<GradeDto, Grade>();

            CreateMap<EmpType, EmpTypeDto>();
            CreateMap<EmpTypeDto, EmpType>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<PermitType, PermitTypeDto>();
            CreateMap<PermitTypeDto, PermitType>();
            CreateMap<PermitState, PermitStateDto>();
            CreateMap<PermitStateDto, PermitState>();
            CreateMap<PermitReleaseConf, PermitReleaseConfDto>();
            CreateMap<PermitReleaseConfDto, PermitReleaseConf>();

            CreateMap<Permit, PermitDto>();
            CreateMap<PermitDto, Permit>();

            CreateMap<PermitCrossRef, PermitCrossRefDto>();
            CreateMap<PermitCrossRefDto, PermitCrossRef>();
            CreateMap<PermitPerson, PermitPersonDto>();
            CreateMap<PermitPersonDto, PermitPerson>();

            CreateMap<PermitHotWork, PermitHotWorkDto>();
            CreateMap<PermitHotWorkDto, PermitHotWork>();
            CreateMap<PermitColdWork, PermitColdWorkDto>();
            CreateMap<PermitColdWorkDto, PermitColdWork>();
            CreateMap<PermitVesselEntry, PermitVesselEntryDto>();
            CreateMap<PermitVesselEntryDto, PermitVesselEntry>();
            CreateMap<PermitElecIsolation, PermitElecIsolationDto>();
            CreateMap<PermitElecIsolationDto, PermitElecIsolation>();
            CreateMap<PermitHeight, PermitHeightDto>();
            CreateMap<PermitHeightDto, PermitHeight>();
            CreateMap<PermitHistory, PermitHistoryDto>();
            CreateMap<PermitHistoryDto, PermitHistory>();
            CreateMap<Permit, PermitHistory>();

            CreateMap<Observation, ObsDto>();
            CreateMap<ObsDto, Observation>();
            CreateMap<ObsHistory,ObsHistoryDto>();
            CreateMap<ObsHistoryDto,ObsHistory>();
        }
    }
}