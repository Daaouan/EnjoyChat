using System.Threading;
using Chat_application.Dtos;
using Chat_application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        //depend. injection
        private readonly ChatService _chatService;
        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("register-user")]
        public IActionResult RegisterUser(UserDto model)
        {
            if(_chatService.AddUserToList(model.Name))
            {
                return NoContent();//user added successful
            }

            return BadRequest("This name is already taken");
        }
    }
}
