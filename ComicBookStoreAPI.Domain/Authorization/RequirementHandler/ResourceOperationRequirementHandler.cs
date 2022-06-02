using ComicBookStoreAPI.Domain.Authorization.Requirements;
using ComicBookStoreAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ComicBookStoreAPI.Domain.Authorization.RequirementHandler
{
    public class ComicBookResourceOperationRequirementHandler : AuthorizationHandler<ComicBookResourceOperationRequirement, Rating>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ComicBookResourceOperationRequirement requirement, Rating resource)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read)
            {
                context.Succeed(requirement);
            }


            string userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                throw new Exception("Unable to get User Id during auth authorization");
            }

            if (requirement.ResourceOperation == ResourceOperation.Update && resource.User.Id == userId)
            {
                context.Succeed(requirement);
            }

            if (requirement.ResourceOperation == ResourceOperation.Create)
            {
                var ratingExists = requirement.comicBook.Ratings.Any(x => x.User.Id == userId);

                if (!ratingExists)
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.ResourceOperation == ResourceOperation.Delete)
            {
                var role = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

                if (role == "Administrator")
                {
                    context.Succeed(requirement);
                }


                if (resource.User.Id == userId)
                {
                    context.Succeed(requirement);
                }
            }


            return Task.CompletedTask;
        }
    }
}
