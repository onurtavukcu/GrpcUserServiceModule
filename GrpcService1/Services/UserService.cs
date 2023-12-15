using Grpc.Core;
using GrpcService1.AppDbContext;
using GrpcService1.Models;
using GrpcService1.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcService1.Services
{
    public class UserService : UserPr.UserPrBase
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            if (request.Name == string.Empty || request.Surname == string.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Değerler boş hacı"));
            }

            var userItem = new UserModel
            {
                name = request.Name,
                surnane = request.Surname,
                emailAdress = request.EmailAdress
            };

            await _dbContext.AddAsync(userItem);

            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new CreateUserResponse
            {
                Id = userItem.id
            });
        }

        public override async Task<ReadUserResponse> ReadUser(ReadUserRequest request, ServerCallContext context)
        {
            if (request.Id <= 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Request ID is NULL!!"));
            }

            var userItem = _dbContext.UserModel.FirstOrDefaultAsync(t => t.id == request.Id);

            try
            {
                return await Task.FromResult(new ReadUserResponse
                {
                    Id = userItem.Result.id,
                    Name = userItem.Result.name,
                    Surnane = userItem.Result.surnane,
                    EmailAdress = userItem.Result.emailAdress
                });
            }
            catch (Exception)
            {

                throw new RpcException(new Status(StatusCode.NotFound, $"User Not Found {request.Id}"));
            }

        }

    }
}

