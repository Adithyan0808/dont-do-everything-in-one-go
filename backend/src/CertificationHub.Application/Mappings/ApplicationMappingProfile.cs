using AutoMapper;
using CertificationHub.Application.Features.Assessments.DTOs;
using CertificationHub.Application.Features.Auth.Commands;
using CertificationHub.Application.Features.Auth.DTOs;
using CertificationHub.Application.Features.Drives.Commands;
using CertificationHub.Application.Features.Drives.DTOs;
using CertificationHub.Application.Features.Notifications.DTOs;
using CertificationHub.Application.Features.Registrations.DTOs;
using CertificationHub.Application.Features.Vouchers.DTOs;
using CertificationHub.Domain.Entities;

namespace CertificationHub.Application.Mappings;

public sealed class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<User, CurrentUserDto>();
        CreateMap<RegisterUserCommand, User>()
            .ForMember(destination => destination.PasswordHash, options => options.Ignore());

        CreateMap<CreateDriveCommand, CertificationDrive>()
            .ForMember(destination => destination.DriveId, options => options.Ignore())
            .ForMember(destination => destination.DriveCode, options => options.Ignore())
            .ForMember(destination => destination.Status, options => options.Ignore())
            .IncludeMembers(source => source.Drive);
        CreateMap<CreateDriveDto, CertificationDrive>();
        CreateMap<CertificationDrive, DriveResponseDto>();
        CreateMap<CertificationDrive, DriveListDto>();

        CreateMap<Registration, RegistrationResponseDto>();
        CreateMap<Registration, RegistrationSummaryDto>()
            .ForMember(destination => destination.DriveName, options => options.MapFrom(source => source.Drive != null ? source.Drive.DriveName : string.Empty))
            .ForMember(destination => destination.CandidateName, options => options.MapFrom(source => source.User != null ? source.User.FullName : string.Empty));

        CreateMap<AssessmentResult, AssessmentResponseDto>();
        CreateMap<AssessmentResult, AssessmentSummaryDto>()
            .ForMember(destination => destination.CandidateName, options => options.MapFrom(source => source.Registration != null && source.Registration.User != null ? source.Registration.User.FullName : string.Empty))
            .ForMember(destination => destination.DriveName, options => options.MapFrom(source => source.Registration != null && source.Registration.Drive != null ? source.Registration.Drive.DriveName : string.Empty));

        CreateMap<Voucher, VoucherResponseDto>();
        CreateMap<Notification, NotificationDto>();
        CreateMap<Notification, NotificationSummaryDto>();
    }
}
