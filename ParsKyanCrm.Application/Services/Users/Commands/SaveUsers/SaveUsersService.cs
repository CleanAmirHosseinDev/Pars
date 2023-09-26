using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common;
using ParsKyanCrm.Common.Dto;
using ParsKyanCrm.Domain.Contexts;
using ParsKyanCrm.Domain.Entities;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Users.Commands.SaveUsers
{

    public class SaveUsersService : ISaveUsersService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;
        public SaveUsersService(IDataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private bool Check_Remote(UserRolesDto request)
        {
            try
            {
                string strCondition = string.Empty;

                if (!string.IsNullOrEmpty(request.User.Mobile))
                {
                    strCondition = " " + nameof(request.User.Mobile) + " = " + "N'" + request.User.Mobile + "'";
                }
                else if (!string.IsNullOrEmpty(request.User.UserName))
                {
                    strCondition = " " + nameof(request.User.UserName) + " = " + "N'" + request.User.UserName + "'";
                }
                else if (!string.IsNullOrEmpty(request.User.Email))
                {
                    strCondition = " " + nameof(request.User.Email) + " = " + "N'" + request.User.Email + "'";
                }

                if (!string.IsNullOrEmpty(strCondition))
                {
                    var q = Ado_NetOperation.GetAll_Table(typeof(Domain.Entities.Users).Name, "*", strCondition + " AND " + nameof(request.UserId) + " != " + request.UserId);
                    return q != null && q.Rows.Count > 0 ? false : true;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Validation_Execute(UserRolesDto request)
        {
            try
            {
                if (!Check_Remote(new UserRolesDto() { UserId = request.UserId, User = new UsersDto() { Mobile = request.User.Mobile } }))
                {
                    return "موبایل مورد نظر از قبل موجود می باشد لطفا موبایل دیگری وارد نمایید";
                }

                if (!Check_Remote(new UserRolesDto() { UserId = request.UserId, User = new UsersDto() { Email = request.User.Email } }))
                {
                    return "پست الکترونیکی مورد نظر از قبل موجود می باشد لطفا پست الکترونیکی دیگری وارد نمایید";
                }

                if (!Check_Remote(new UserRolesDto() { UserId = request.UserId, User = new UsersDto() { UserName = request.User.UserName } }))
                {
                    return "نام کاربری مورد نظر از قبل موجود می باشد لطفا نام کاربری دیگری وارد نمایید";
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultDto<UserRolesDto>> Execute(UserRolesDto request)
        {
            try
            {
                #region Validation

                string strValidation = Validation_Execute(request);
                if (!string.IsNullOrEmpty(strValidation))
                {
                    return new ResultDto<UserRolesDto>()
                    {
                        IsSuccess = false,
                        Message = strValidation,
                        Data = null
                    };
                }

                #endregion

                EntityEntry<UserRoles> q_Entity;
                if (request.UserId == 0)
                {
                    request.User.Ip = await Ipconfig.GetUserHostAddress();
                    request.User.Password = EncryptDecrypt.Encrypt(request.User.Password);
                    q_Entity = _context.UserRoles.Add(new UserRoles()
                    {
                        RoleId = request.RoleId,
                        User = new Domain.Entities.Users()
                        {
                            IsActive = (byte)Common.Enums.TablesGeneralIsActive.Active,
                            Email = request.User.Email,
                            Ip = request.User.Ip,
                            Mobile = request.User.Mobile,
                            Password = request.User.Password,
                            Status = request.User.Status,
                            UserName = request.User.UserName,
                            RealName = request.User.RealName
                        }
                    });
                    await _context.SaveChangesAsync();
                    request = _mapper.Map<UserRolesDto>(q_Entity.Entity);
                }
                else
                {
                    Ado_NetOperation.SqlUpdate(typeof(Domain.Entities.Users).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.User.UserName),request.User.UserName
                        },
                        {
                            nameof(q_Entity.Entity.User.Ip),await Ipconfig.GetUserHostAddress()
                        },
                        {
                            nameof(q_Entity.Entity.User.Status),request.User.Status
                        },
                        {
                            nameof(q_Entity.Entity.User.Mobile),request.User.Mobile
                        },
                        {
                            nameof(q_Entity.Entity.User.Email),request.User.Email
                        },
                        {
                            nameof(q_Entity.Entity.User.RealName),request.User.RealName
                        },
                    }, string.Format(nameof(q_Entity.Entity.UserId) + " = {0} ", request.UserId));
                    Ado_NetOperation.SqlUpdate(typeof(UserRoles).Name, new Dictionary<string, object>()
                    {
                        {
                            nameof(q_Entity.Entity.RoleId),request.RoleId
                        }
                    }, string.Format(nameof(q_Entity.Entity.UserId) + " = {0} ", request.UserId));
                }

                return new ResultDto<UserRolesDto>()
                {
                    IsSuccess = true,
                    Message = "ثبت کاربر با موفقیت انجام شد",
                    Data = request
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
