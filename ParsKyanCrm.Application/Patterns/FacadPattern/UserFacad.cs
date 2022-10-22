using AutoMapper;
using ParsKyanCrm.Domain.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using ParsKyanCrm.Application.Services.Users.Queries.GetUserss;
using ParsKyanCrm.Application.Services.Users.Queries.GetUsers;
using ParsKyanCrm.Application.Services.Users.Commands.SaveUsers;
using ParsKyanCrm.Application.Services.Users.Queries.GetRoless;
using ParsKyanCrm.Application.Services.Users.Queries.GetAccessLevels;
using ParsKyanCrm.Application.Services.Users.Commands.SaveAccessLevels;
using ParsKyanCrm.Application.Services.Users.Commands.SaveBasicInformationCustomers;
using ParsKyanCrm.Application.Services.Users.Queries.GetCustomers;
using FluentValidation;
using ParsKyanCrm.Application.Dtos.Users;

namespace ParsKyanCrm.Application.Patterns.FacadPattern
{
    public interface IUserFacad
    {
        IGetUserssService GetUserssService { get; }

        IGetUsersService GetUsersService { get; }

        ISaveUsersService SaveUsersService { get; }

        IGetRolessService GetRolessService { get; }

        IGetAccessLevelsService GetAccessLevelsService { get; }

        ISaveAccessLevelsService SaveAccessLevelsService { get; }

        ISaveBasicInformationCustomersService SaveBasicInformationCustomersService { get; }

        IGetCustomersService GetCustomersService { get; }

    }

    public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        private readonly IBasicInfoFacad _basicInfoFacad;
        private readonly IWebHostEnvironment _env;
        private readonly IValidator<CustomersDto> _validator;

        public UserFacad(IDataBaseContext context, IMapper mapper, IBasicInfoFacad basicInfoFacad, IWebHostEnvironment env, IValidator<CustomersDto> validator)
        {
            _context = context;
            _mapper = mapper;
            _basicInfoFacad = basicInfoFacad;
            _env = env;
            _validator = validator;
        }

        private IGetUserssService _getUserssService;
        public IGetUserssService GetUserssService
        {
            get
            {
                return _getUserssService = _getUserssService ?? new GetUserssService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetUsersService _getUsersService;
        public IGetUsersService GetUsersService
        {
            get
            {
                return _getUsersService = _getUsersService ?? new GetUsersService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveUsersService _saveUsersService;
        public ISaveUsersService SaveUsersService
        {
            get
            {
                return _saveUsersService = _saveUsersService ?? new SaveUsersService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetRolessService _getRolessService;
        public IGetRolessService GetRolessService
        {
            get
            {
                return _getRolessService = _getRolessService ?? new GetRolessService(_context, _mapper, _basicInfoFacad);
            }
        }

        private IGetAccessLevelsService _getAccessLevelsService;
        public IGetAccessLevelsService GetAccessLevelsService
        {
            get
            {
                return _getAccessLevelsService = _getAccessLevelsService ?? new GetAccessLevelsService(_context, _mapper, _basicInfoFacad);
            }
        }

        private ISaveAccessLevelsService _saveAccessLevelsService;
        public ISaveAccessLevelsService SaveAccessLevelsService
        {
            get
            {
                return _saveAccessLevelsService = _saveAccessLevelsService ?? new SaveAccessLevelsService();
            }
        }

        private ISaveBasicInformationCustomersService _saveBasicInformationCustomersService;
        public ISaveBasicInformationCustomersService SaveBasicInformationCustomersService
        {
            get
            {
                return _saveBasicInformationCustomersService = _saveBasicInformationCustomersService ?? new SaveBasicInformationCustomersService(_context, _mapper, _basicInfoFacad, _validator);
            }
        }

        private IGetCustomersService _getCustomersService;
        public IGetCustomersService GetCustomersService
        {
            get
            {
                return _getCustomersService = _getCustomersService ?? new GetCustomersService(_context, _mapper, _basicInfoFacad);
            }
        }

    }
}
