using System;
using System.Threading.Tasks;
using AutoMapper;
using BookReview.Application.Dtos;
using BookReview.Application.Interfaces;
using BookReview.Domain;
using BookReview.Persistence.Interfaces;
using BCryptNet = BCrypt.Net.BCrypt;

namespace BookReview.Application
{
    public class UserService : IUserService
    {
        public readonly IUserPersist _userPersist;
        public readonly IGeralPersist _geralPesist;
        public readonly IMapper _mapper;

        public UserService(IUserPersist userPersist, IGeralPersist geralPersist, IMapper mapper)
        {
            _userPersist = userPersist;
            _geralPesist = geralPersist;
            _mapper = mapper;
        }
        public async Task<UserDto> GetAllUsersByEmailAsync(string email)
        {
            try
            {
                var user = await _userPersist.GetAllUsersByEmailAsync(email);
                if (user == null)
                {
                    return null;
                }

                var userDto = _mapper.Map<UserDto>(user);
                return userDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserNoPasswordDto[]> GetAllUsersByNameAsync(string name)
        {
            try
            {
                var users = await _userPersist.GetAllUsersByNameAsync(name);
                if (users == null)
                {
                    return null;
                }

                var usersDto = _mapper.Map<UserNoPasswordDto[]>(users);
                return usersDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserNoPasswordDto[]> GetAllUsersAsync()
        {
            try
            {
                var users = await _userPersist.GetAllUsersAsync();
                if (users == null)
                {
                    return null;
                }

                var usersDto = _mapper.Map<UserNoPasswordDto[]>(users);
                return usersDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserNoPasswordDto> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userPersist.GetUserByIdAsync(id);
                if (user == null)
                {
                    return null;
                }

                var userNoPasswordDto = _mapper.Map<UserNoPasswordDto>(user);
                return userNoPasswordDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDto> AddUser(UserDto model)
        {
            try
            {
                var user = _mapper.Map<User>(model);

                user.Password = BCryptNet.HashPassword(user.Password);

                _geralPesist.Add<User>(user);
                if (await _geralPesist.SaveChangeAsync())
                {
                    var userFind = await _userPersist.GetUserByIdAsync(user.Id);
                    var returnUser = _mapper.Map<UserDto>(userFind);
                    return returnUser;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                var user = await _userPersist.GetUserByIdAsync(userId);
                if (user == null) {
                    return false;
                }

                _geralPesist.Delete<User>(user);

                if (await _geralPesist.SaveChangeAsync()) {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDto> UpdateUser(UserDto model, int userId)
        {
            try
            {
                var originalUserData = await _userPersist.GetUserByIdAsync(userId);

                model.Id = originalUserData.Id;

                var modelToUpdate = _mapper.Map<User>(model);

                _geralPesist.Update<User>(modelToUpdate);

                if (await _geralPesist.SaveChangeAsync())
                {
                    var userFind = await _userPersist.GetUserByIdAsync(modelToUpdate.Id);
                    var returnUser = _mapper.Map<UserDto>(userFind);
                    return returnUser;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}