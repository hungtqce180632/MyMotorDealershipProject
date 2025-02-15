namespace MyMotorDealership.Services.Posts
{
    using System;
    using System.Linq;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Motors;
    using Motors.Models;
    using Data;
    using Data.Models;
    using Images;
    using Images.Models;
    using Models;

    public class PostsService : IPostsService
    {
        private readonly MotorDealershipDbContext data;
        private readonly IMotorsService MotorsService;
        private readonly IImagesService imagesService;

        public PostsService(MotorDealershipDbContext data, IMotorsService MotorsService, IImagesService imagesService)
        {
            this.data = data;
            this.MotorsService = MotorsService;
            this.imagesService = imagesService;
        }

        public async Task<int> CreateAsync(PostFormInputModelDTO inputPost, Motor Motor, string userId, bool isPublic)
        {
            var post = new Post
            {
                Motor = Motor,
                CreatorId = userId,
                PublishedOn = DateTime.UtcNow,
                SellerName = inputPost.SellerName,
                SellerPhoneNumber = inputPost.SellerPhoneNumber,
                IsPublic = isPublic,
            };

            await this.data.Posts.AddAsync(post);
            await this.data.SaveChangesAsync();

            return post.Id;
        }

        public IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage)
        {
            return posts.Skip((page - 1) * postsPerPage).Take(postsPerPage).ToList();
        }

        public IEnumerable<PostByUserDTO> GetPostsByUser(string userId, int sortingNumber)
        {
            var postsQuery = this.data.Posts
                .Where(p => p.CreatorId == userId && !p.IsDeleted).AsQueryable();
            
            postsQuery = GetSortedPosts(postsQuery, sortingNumber);

            var posts = postsQuery    
                .Select(p => new PostByUserDTO()
                {
                    Motor = new MotorByUserDTO()
                    {
                        Id = p.Motor.Id,
                        Make = p.Motor.Make,
                        Model = p.Motor.Model,
                        Price = p.Motor.Price,
                        Year = p.Motor.Year,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Motor.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).ToList();

            return posts;
        }

        public IEnumerable<PostInListDTO> GetMatchingPosts(SearchPostDTO searchInputModel, int sortingNumber)
        {
            var postsQuery = this.data.Posts.Where(p => !p.IsDeleted && p.IsPublic).AsQueryable();
            
            if (searchInputModel.Motor != null)
            {
                var searchedMotorDetails = searchInputModel.Motor;

                if (!string.IsNullOrWhiteSpace(searchedMotorDetails.TextSearchTerm))
                {
                    postsQuery = postsQuery.Where(p =>
                        (p.Motor.Make + " " + p.Motor.Model + " " + p.Motor.Description).ToLower()
                        .Contains(searchedMotorDetails.TextSearchTerm.ToLower()));
                }

                if (searchedMotorDetails.FromYear is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Motor.Year >= searchedMotorDetails.FromYear);
                }

                if (searchedMotorDetails.ToYear is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Motor.Year <= searchedMotorDetails.ToYear);
                }

                if (searchedMotorDetails.MinHorsepower is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Motor.Horsepower >= searchedMotorDetails.MinHorsepower);
                }

                if (searchedMotorDetails.MaxHorsepower is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Motor.Horsepower <= searchedMotorDetails.MaxHorsepower);
                }

                if (searchedMotorDetails.MinPrice is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Motor.Price >= searchedMotorDetails.MinPrice);
                }

                if (searchedMotorDetails.MaxPrice is > 0)
                {
                    postsQuery = postsQuery.Where(p =>
                        p.Motor.Price <= searchedMotorDetails.MaxPrice);
                }
            }

            if (searchInputModel.SelectedCategoriesIds.Any())
            {
                postsQuery = postsQuery.Where(p => searchInputModel.SelectedCategoriesIds.Contains(p.Motor.CategoryId));
            }

            if (searchInputModel.SelectedFuelTypesIds.Any())
            {
                postsQuery = postsQuery.Where(p => searchInputModel.SelectedFuelTypesIds.Contains(p.Motor.FuelTypeId));
            }

            if (searchInputModel.SelectedTransmissionTypesIds.Any())
            {
                postsQuery = postsQuery.Where(p => searchInputModel.SelectedTransmissionTypesIds.Contains(p.Motor.TransmissionTypeId));
            }

            if (searchInputModel.SelectedExtrasIds.Any())
            {
                var searchedExtrasIds = searchInputModel.SelectedExtrasIds;
                var currentQueuedMotors = postsQuery.Select(p => p.Motor).ToList();
                var allMatchedMotorIds = new List<int>();

                
                foreach (var Motor in currentQueuedMotors)
                {
                    var currentMotorExtrasIds = data.MotorExtras
                                                            .Where(ce => ce.Motor.Id == Motor.Id)
                                                            .Select(ce => ce.ExtraId)
                                                            .ToList();

                    //The below code checks if all the searched extras are contained in the current Motor extras
                    if (searchedExtrasIds.Intersect(currentMotorExtrasIds).Count() == searchedExtrasIds.Count())
                    {
                        allMatchedMotorIds.Add(Motor.Id);
                    }
                }

                postsQuery = postsQuery.Where(p => allMatchedMotorIds.Contains(p.Motor.Id));
            }

            if (!postsQuery.Any())
            {
                throw new Exception("Unfortunately, there are no Motors in our system that match this search criteria.");
            }

            postsQuery = GetSortedPosts(postsQuery, sortingNumber);

            var posts = postsQuery
                .Select(p => new PostInListDTO()
                {
                    Motor = new MotorInListDTO()
                    {
                        Id = p.Motor.Id,
                        Make = p.Motor.Make,
                        Model = p.Motor.Model,
                        Description = p.Motor.Description.Length <= 100 ?
                            p.Motor.Description
                            :
                            p.Motor.Description.Substring(0, 100) + "...",
                        Price = p.Motor.Price,
                        Year = p.Motor.Year,
                        Kilometers = p.Motor.Kilometers,
                        FuelType = p.Motor.FuelType.Name,
                        TransmissionType = p.Motor.TransmissionType.Name,
                        Category = p.Motor.Category.Name,
                        LocationCity = p.Motor.LocationCity,
                        LocationCountry = p.Motor.LocationCountry,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Motor.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).ToList();

            return posts;
        }

        public int GetAllPostsCount()
        {
            return this.data.Posts.Count();
        }

        public IEnumerable<BasePostInListDTO> GetAllPostsBaseInfo(int page, int postsPerPage)
        {
            var posts = this.data.Posts
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.IsPublic)
                .ThenByDescending(p => p.PublishedOn)
                .Skip((page - 1) * postsPerPage).Take(postsPerPage)
                .Select(p => new BasePostInListDTO()
                {
                    Motor = new BaseMotorDTO()
                    {
                        Id = p.Motor.Id,
                        Make = p.Motor.Make,
                        Model = p.Motor.Model,
                        Year = p.Motor.Year,
                        Price = p.Motor.Price,
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                    IsPublic = p.IsPublic
                }).ToList();

            return posts;
        }

        public SinglePostDTO GetSinglePostViewModelById(int postId, bool publicOnly = true)
        {
            var post = this.data.Posts
                .Where(p => p.Id == postId && !p.IsDeleted && (!publicOnly || p.IsPublic))
                .Select(p => new SinglePostDTO()
                {
                    Motor = new SingleMotorDTO()
                    {
                        Id = p.Motor.Id,
                        Make = p.Motor.Make,
                        Model = p.Motor.Model,
                        Description = p.Motor.Description,
                        Price = p.Motor.Price,
                        Year = p.Motor.Year,
                        Kilometers = p.Motor.Kilometers,
                        Horsepower = p.Motor.Horsepower,
                        FuelType = p.Motor.FuelType.Name,
                        TransmissionType = p.Motor.TransmissionType.Name,
                        Category = p.Motor.Category.Name,
                        LocationCity = p.Motor.LocationCity,
                        LocationCountry = p.Motor.LocationCountry,
                        ComfortExtras = p.Motor.MotorExtras.Where(ce => ce.Extra.TypeId == 1).Select(ce => ce.Extra.Name).ToList(),
                        SafetyExtras = p.Motor.MotorExtras.Where(ce => ce.Extra.TypeId == 2).Select(ce => ce.Extra.Name).ToList(),
                        OtherExtras = p.Motor.MotorExtras.Where(ce => ce.Extra.TypeId == 3).Select(ce => ce.Extra.Name).ToList(),
                        Images = p.Motor.Images.OrderByDescending(img => img.IsCoverImage)
                                             .Select(img => this.imagesService.GetDefaultMotorImagesPath(img.Id, img.Extension))
                                             .ToList(),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                    SellerName = p.SellerName,
                    SellerPhoneNumber = p.SellerPhoneNumber
                })
                .FirstOrDefault();

            return post;
        }

        public EditPostDTO GetPostFormInputModelById(int postId)
        {
            var post = this.data.Posts
                .Where(p => p.Id == postId && !p.IsDeleted)
                .Select(p => new EditPostDTO()
                {
                    Motor = new MotorFormInputModelDTO()
                    {
                        Make = p.Motor.Make,
                        Model = p.Motor.Model,
                        Description = p.Motor.Description,
                        Price = p.Motor.Price,
                        Year = p.Motor.Year,
                        Kilometers = p.Motor.Kilometers,
                        Horsepower = p.Motor.Horsepower,
                        CategoryId = p.Motor.CategoryId,
                        FuelTypeId = p.Motor.FuelTypeId,
                        TransmissionTypeId = p.Motor.TransmissionTypeId,
                        LocationCity = p.Motor.LocationCity,
                        LocationCountry = p.Motor.LocationCountry,
                    },
                    SelectedExtrasIds = p.Motor.MotorExtras.Select(ce => ce.ExtraId).ToList(),
                    SellerName = p.SellerName,
                    SellerPhoneNumber = p.SellerPhoneNumber,
                    CreatorId = p.CreatorId,
                    CurrentImages = p.Motor.Images.OrderByDescending(img => img.IsCoverImage)
                                                .Select(img => new ImageInfoDTO()
                                                        {
                                                            Id = img.Id,
                                                            Path = this.imagesService.GetDefaultMotorImagesPath(img.Id, img.Extension),
                                                        }).ToList(),
                    SelectedCoverImageId = p.Motor.Images.FirstOrDefault(img => img.IsCoverImage).Id,
                    MotorId = p.MotorId,
                }).FirstOrDefault();

            return post;
        }

        public IEnumerable<ImageInfoDTO> GetCurrentDbImagesForAPost(int postId)
        {
             var post = this.data.Posts.FirstOrDefault(p => p.Id == postId && !p.IsDeleted);
             var Motor = this.data.Motors.FirstOrDefault(c => c.Id == post.MotorId && !c.IsDeleted);
             var postImages = this.data.Images
                                        .Where(img => img.MotorId == Motor.Id)
                                        .OrderByDescending(img => img.IsCoverImage)
                                        .Select(img => new ImageInfoDTO()
                                        {
                                            Id = img.Id, 
                                            Path = this.imagesService.GetDefaultMotorImagesPath(img.Id, img.Extension),
                                        }).ToList();

             return postImages;
        }

        public IEnumerable<PostInLatestListDTO> GetLatest(int count)
        {
            var posts = this.data.Posts
                .Where(p => !p.IsDeleted && p.IsPublic)
                .OrderByDescending(p => p.PublishedOn)
                .Take(count)
                .Select(p => new PostInLatestListDTO()
                {
                    Motor = new LatestPostsMotorDTO()
                    {
                        Id = p.Motor.Id,
                        Make = p.Motor.Make,
                        Model = p.Motor.Model,
                        Price = p.Motor.Price,
                        Year = p.Motor.Year,
                        Horsepower = p.Motor.Horsepower,
                        FuelType = p.Motor.FuelType.Name,
                        TransmissionType = p.Motor.TransmissionType.Name,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Motor.Images.ToList())
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).ToList();

            return posts;
        }

        public async Task UpdateAsync(int postId, EditPostDTO editedPost, bool isPublic)
        {
            var post = this.GetDbPostById(postId);

            if (post == null)
            {
                throw new Exception($"Unfortunately, we cannot find such post in our system!");
            }
            
            post.ModifiedOn = DateTime.UtcNow;
            post.SellerName = editedPost.SellerName;
            post.SellerPhoneNumber = editedPost.SellerPhoneNumber;
            post.IsPublic = isPublic;
            
            await this.data.SaveChangesAsync();
        }

        public async Task ChangeVisibilityAsync(int postId)
        {
            var post = this.GetDbPostById(postId);

            post.IsPublic = !post.IsPublic;

            await this.data.SaveChangesAsync();
        }

        public PostByUserDTO GetBasicPostInformationById(int postId)
        {
            var post = this.data.Posts
                .Where(p => p.Id == postId && !p.IsDeleted)
                .Select(p => new PostByUserDTO()
                {
                    Motor = new MotorByUserDTO()
                    {
                        Id = p.Motor.Id,
                        Make = p.Motor.Make,
                        Model = p.Motor.Model,
                        Price = p.Motor.Price,
                        Year = p.Motor.Year,
                        CoverImage = this.imagesService.GetCoverImagePath(p.Motor.Images.ToList()),
                    },
                    PublishedOn = GetFormattedDate(p.PublishedOn),
                }).FirstOrDefault();

            return post;
        }

        public string GetPostCreatorId(int postId)
        {
            var post = this.GetDbPostById(postId);

            return post?.CreatorId;
        }

        public async Task DeletePostByIdAsync(int postId)
        {
            var post = this.GetDbPostById(postId);

            if (post == null)
            {
                throw new Exception($"Unfortunately, we cannot find such post in our system!");
            }

            await this.MotorsService.DeleteMotorByIdAsync(post.Id);

            post.IsDeleted = true;
            post.DeletedOn = DateTime.UtcNow;
            post.IsPublic = false;
            
            await this.data.SaveChangesAsync();
        }

        private Post GetDbPostById(int postId)
        {
            return this.data.Posts.FirstOrDefault(p => p.Id == postId && !p.IsDeleted);
        }

        private static string GetFormattedDate(DateTime inputDateTime)
        {
            if (inputDateTime.Date == DateTime.UtcNow.Date)
            {
                return "Today, " + inputDateTime.ToString("t", CultureInfo.InvariantCulture);
            }

            return inputDateTime.ToString("d", CultureInfo.InvariantCulture);
        }
        
        private static IQueryable<Post> GetSortedPosts(IQueryable<Post> postsQuery, int sortingNumber)
        {
            postsQuery = sortingNumber switch
            {
                1 => postsQuery.OrderBy(p => p.Id),
                2 => postsQuery.OrderByDescending(p => p.Motor.Price),
                3 => postsQuery.OrderBy(p => p.Motor.Price),
                4 => postsQuery.OrderByDescending(p => p.Motor.Horsepower),
                5 => postsQuery.OrderBy(p => p.Motor.Horsepower),
                6 => postsQuery.OrderByDescending(p => p.Motor.Year),
                7 => postsQuery.OrderBy(p => p.Motor.Year),
                _ => postsQuery.OrderByDescending(p => p.Id),
            };

            return postsQuery;
        }
    }
}