﻿namespace Project.API.DTO
{
    public class UserDTO
    {
        public string username {  get; set; }
        public string password { get; set; }

        public UserDTO(string? username, string? password)
        {
            this.username = username;
            this.password = password;
        }


    }
}
