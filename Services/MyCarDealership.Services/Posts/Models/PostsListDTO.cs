﻿namespace MyMotorDealership.Services.Posts.Models
{
    using System.Collections.Generic;

    public class PostsListDTO
    {
        public IEnumerable<PostInListDTO> Posts { get; init; }
    }
}