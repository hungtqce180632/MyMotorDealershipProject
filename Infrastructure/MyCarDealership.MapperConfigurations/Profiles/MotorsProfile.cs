namespace MyMotorDealership.MapperConfigurations.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Services.Motors.Models;
    using Web.ViewModels.Motors;

    public class MotorsProfile : Profile
    {
        public MotorsProfile()
        {
            this.CreateMap<Category, BaseMotorSpecificationServiceModel>();

            this.CreateMap<FuelType, BaseMotorSpecificationServiceModel>();

            this.CreateMap<TransmissionType, BaseMotorSpecificationServiceModel>();

            this.CreateMap<Extra, MotorExtrasServiceModel>();

            this.CreateMap<BaseMotorSpecificationServiceModel, BaseMotorSpecificationViewModel>().ReverseMap();

            this.CreateMap<MotorExtrasServiceModel, MotorExtrasViewModel>().ReverseMap();

            this.CreateMap<BaseMotorDTO, BaseMotorViewModel>().ReverseMap();

            this.CreateMap<MotorFormInputModelDTO, MotorFormInputModel>().ReverseMap();

            this.CreateMap<SearchMotorInputModelDTO, SearchMotorInputModel>().ReverseMap();

            this.CreateMap<MotorInListDTO, MotorInListViewModel>().ReverseMap();

            this.CreateMap<SingleMotorDTO, SingleMotorViewModel>().ReverseMap();

            this.CreateMap<MotorByUserDTO, MotorByUserViewModel>().ReverseMap();

            this.CreateMap<LatestPostsMotorDTO, LatestPostsMotorViewModel>().ReverseMap();
        }
    }
}