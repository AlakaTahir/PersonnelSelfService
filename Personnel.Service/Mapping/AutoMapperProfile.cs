using AutoMapper;
using Personnel.Model.Entity;
using Personnel.Model.ViewModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Personnel.Service.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            Config();
        }
        public void Config()
        {
            CreateMap<EmployeeInfo, EmployeeResponseModel>().ReverseMap();
        }
    }
}
