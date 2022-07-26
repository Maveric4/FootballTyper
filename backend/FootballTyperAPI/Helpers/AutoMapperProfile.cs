namespace FootballTyperAPI.Helpers;

using AutoMapper;
using FootballTyperAPI.Models;
using FootballTyperAPI.Models.Users;
using FootballTyperAPI.Models.Bets;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse
        CreateMap<TyperUser, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, TyperUser>();

        // UpdateRequest -> User
        CreateMap<UpdateRequest, TyperUser>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // UpdateImgLinkRequest -> User
        CreateMap<UpdateImgLinkRequest, TyperUser>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // UpdateFullNameRequest -> User
        CreateMap<UpdateFullNameRequest, TyperUser>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // PostBetRequest -> Bet
        CreateMap<PostBetRequest, Bet>();

        // PutBetRequest -> Bet
        CreateMap<PutBetRequest, Bet>();


        // Bet -> GetBetRequest
        CreateMap<Bet, GetBetRequest>();

        //TyperUser->TyperUserApi
        CreateMap<TyperUser, TyperUserApi>();

        //TyperUserApi->TyperUser
        CreateMap<TyperUserApi, TyperUser>();

        // GoogleLoginRequest -> User
        CreateMap<GoogleLoginRequest, TyperUser>();

    }
}