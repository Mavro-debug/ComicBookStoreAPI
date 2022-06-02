using ComicBookStoreAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ComicBookStoreAPI.Domain.Authorization.Requirements
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class ComicBookResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get; }
        public ComicBook comicBook { get; }

        public ComicBookResourceOperationRequirement(ResourceOperation resourceOperation, ComicBook comicBook)
        {
            ResourceOperation = resourceOperation;
            this.comicBook = comicBook;
        }
    }
}
