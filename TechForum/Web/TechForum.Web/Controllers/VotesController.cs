using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechForum.Data.Models;
using TechForum.Services.Data;
using TechForum.Web.ViewModels.Votes;

namespace TechForum.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService voteService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(IVoteService voteService, UserManager<ApplicationUser> userManager)
        {
            this.voteService = voteService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.voteService.VoteAsync(input.PostId, user.Id, input.IsUpVote);

            var votes = this.voteService.GetVotes(input.PostId);
            return new VoteResponseModel { VotesCount = votes };

        }
    }
}
