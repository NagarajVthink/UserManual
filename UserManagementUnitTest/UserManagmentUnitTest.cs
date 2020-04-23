using Microsoft.Extensions.Configuration;
using UserManagement.Models;
using UserManagement.Models.Users;
using UserManagement.Services;
using Xunit;

namespace UserManagmentUnitTest
{
    public class UserManagmentUnitTest
    {
        //Unit Test SearchByUserUID api using two valid and invalid userUID
        [Theory]
        [InlineData("74EA5BC3-7B41-4A2D-ACD2-2B5C98F51562", true)]
        [InlineData("5B95D8CC-599A-43F8-8795-11C5E722D939", false)]
        public void SearchByUserUIDTest(string userUID, bool isValid)
        {
            //Assemble
            var appConfig = GetConfiguration();
            var service = new User(appConfig);

            // Act 
            var result = service.SearchUserDetailByUserUID(userUID);

            //Assert
            Assert.Equal(result.Success, isValid);

        }

        //Unit Test Search api using IsActive param
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Search(bool isValid)
        {
            //Assemble
            var appConfig = GetConfiguration();
            var service = new User(appConfig);

            var searchRequest = new SearchRequest
            {
                StartIndex = 0,
                EndIndex = 10,
                Email = "anand@vthink.co.in",
                Phone = "98230909",
                FirstName = "",
                LastName = "",
                MiddleName = "",
                City = "",
                State = "",
                Country = "",
                Postal = "",
                IsActive = true
            };
            switch (isValid)
            {
                case true:
                    searchRequest.IsActive = true;
                    break;
                case false:
                    searchRequest.IsActive = false;
                    break;
            }

            // Act 
            var result = service.SearchUserDetail(searchRequest);

            //Assert
            Assert.Equal(result.Success, isValid);
        }


        //Unit Test CreateUser api using Same Email
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CreateUser(bool isValid)
        {
            //Assemble
            var appConfig = GetConfiguration();
            var service = new User(appConfig);

            var createUser = new CreateUser
            {
                CreatedBy = "Testing1",
                Email = "test@test2.com",
                Phone = "1234567890",
                FirstName = "test",
                MiddleName = "",
                LastName = "T",
                City = "Chennai",
                State = "Tamilnadu",
                Country = "India",
                Postal = "600091"
            };

            // Act 
            var result = service.InsertUserDetails(createUser);

            //Assert
            Assert.Equal(result.Success, isValid);
        }

        //Unit Test UpdateUser api using one valid and invalid userUID
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UpdateUser(bool isValid)
        {
            //Assemble
            var appConfig = GetConfiguration();
            var service = new User(appConfig);

            var updateUser = new UpdateUser
            {
                UpdatedBy = "Testing1",
                Email = "test2@test2.com",
                Phone = "1234567890",
                FirstName = "test",
                MiddleName = "",
                LastName = "T",
                City = "Chennai",
                State = "Tamilnadu",
                Country = "India",
                Postal = "600091"
            };

            switch (isValid)
            {
                case true:
                    updateUser.UserUID = "74EA5BC3-7B41-4A2D-ACD2-2B5C98F51562";
                    break;
                case false:
                    updateUser.UserUID = "74EA5BC3-7B41-4A2D-ACD2-2B5C98F51569";
                    break;
            }

            // Act 
            var result = service.UpdateUserDetailByUserUID(updateUser);

            //Assert
            Assert.Equal(result.Success, isValid);
        }

        //Unit Test UpdateUser api using one valid and invalid userUID
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DeleteUser(bool isValid)
        {
            //Assemble
            var appConfig = GetConfiguration();
            var service = new User(appConfig);

            var deleteUser = new DeleteUser
            {
                UpdatedBy = "Testing1"
            };

            switch (isValid)
            {
                case true:
                    deleteUser.UserUID = "74EA5BC3-7B41-4A2D-ACD2-2B5C98F51562";
                    break;
                case false:
                    deleteUser.UserUID = "74EA5BC3-7B41-4A2D-ACD2-2B5C98F51560";
                    break;
            }

            // Act 
            var result = service.InActiveUserByUID(deleteUser);

            //Assert
            Assert.Equal(result.Success, isValid);
        }

        //Read apisetting.json
        public static AppConfig GetConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config.GetSection("AppConfig").Get<AppConfig>(); ;
        }
    }
}
